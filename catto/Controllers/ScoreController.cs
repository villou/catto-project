using catto.DTO;
using catto.Models;
using catto.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoreController : ControllerBase
{
  private readonly CattoContext _context;
  private readonly UserProvider _userProvider;

  public ScoreController(CattoContext context, UserProvider userProvider)
  {
    _context = context;
    _userProvider = userProvider;
  }

  [HttpGet]
  public List<ScoreIndexDto> Get()
  {
    return _context.Scores
        .Include(s => s.User)
        .OrderByDescending(s => s.Value)
        .Select(s => new ScoreIndexDto { Score = s.Value, Username = s.User.Username ?? "", Avatar = s.User.Avatar ?? "https://64.media.tumblr.com/3a1730fccc0f8144e4823b333383855d/tumblr_ozu6r9kdg31rxye79o1_1280.jpg" })
        .Take(5)
        .ToList();
  }

  [HttpPost]
  public async Task<ActionResult> Post(ScoreDto scoreDto)
  {
    var user = await _userProvider.GetUserFromToken(HttpContext);

    if (user == null) return Unauthorized();

    var score = new Score
    {
      Value = scoreDto.Score,
      UserId = user.Id
    };
    _context.Scores.Add(score);
    await _context.SaveChangesAsync();
    return NoContent();
  }

  [HttpGet("{userId:int}")]
  public async Task<ActionResult<List<ScoreDto>>> GetScoreByUserId(int userId)
  {
    var user = _userProvider.GetUserFromToken(HttpContext);
    if (user.Id != userId)
    {
      return Unauthorized();
    }

    var scores = await _context.Scores.Where(s => s.UserId == userId).ToListAsync();

    return Ok(scores);
  }
}