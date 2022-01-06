using Microsoft.AspNetCore.Mvc;
using catto.Models;
using Microsoft.EntityFrameworkCore;

namespace catto.Controllers;

[ApiController]
[Route("[api/controller]")]
public class UserController : ControllerBase
{

  private readonly CattoContext _context;
  private readonly UserProvider _userProvider;

  public UserController(CattoContext context, UserProvider userProvider)
  {
    _context = context;
    _userProvider = userProvider;
  }

  [HttpGet("me")]
  public async Task<ActionResult<User>> GetMe()
  {
    var user = await _userProvider.GetUserFromToken(HttpContext);
    return user == null ? Unauthorized() : user;
  }

  // GET: api/User
  [HttpGet]
  public List<User> Get()
  {
    return _context.Users.ToList();
  }

  // GET: api/User/5
  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUserById(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    return user;

  }

  [HttpPatch]
  public async Task<ActionResult<User>> UpdateUser(User user)
  {
    _context.Entry(user).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return user;
  }


  // créer un dto avec uniquement les champs qui sont requis (username, password)
  [HttpPost("login")]
  public async Task<ActionResult<User>> Login([FromBody] User user)
  {
    var userLogin = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
    if (userLogin == null)
    {
      return NotFound();
    }
    return userLogin;
  }

  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(User user)
  {
    var createdUser = await _userProvider.Register(user);

    var token = await _context.Tokens.AddAsync(Token.GenerateToken(createdUser.Id));

    HttpContext.Response.Cookies.Append("Token", token.Entity.TokenKey);

    return createdUser;
  }

  [HttpPost("logout")]
  public async Task<ActionResult<User>> Logout()
  {
    return null;
  }

}