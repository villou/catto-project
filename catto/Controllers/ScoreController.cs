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
        //distinct
        return _context.Scores
            .Include(s => s.User)
            .OrderByDescending(s => s.Value)
            .Select(s => new ScoreIndexDto { Score = s.Value, Username = s.User.Username ?? "" })
            .Take(5)
            .ToList();
    }

    [HttpPost("save")]
    public async Task<ActionResult> SaveScore(ScoreDto scoreDto)
    {
        var user = await _userProvider.GetUserFromToken(HttpContext);

        var score = new Score
        {
            Value = scoreDto.Value,
            UserId = user.Id
        };
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<List<Score>>> GetScoreByUserId(int userId)
    {
        var user = _userProvider.GetUserFromToken(HttpContext);
        if (user.Id != userId)
        {
            return Unauthorized();
        }

        var scores =  await _context.Scores.Where(s => s.UserId == userId).ToListAsync();
        
        return Ok(scores);
    }
}