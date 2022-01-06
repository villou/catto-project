using catto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catto.Controllers;

[ApiController]
[Route("[api/controller]")]
public class ScoreController : ControllerBase
{
    private readonly CattoContext _context;

    public ScoreController(CattoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<Score> Get()
    {
        return _context.Scores.ToList();
    }

    // GET api/score/5
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Score>>> GetScoreByUserId(int userId)
    {
        return await _context.Scores.Where(s => s.UserId == userId).ToListAsync();
    }
}