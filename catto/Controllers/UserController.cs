using catto.DTO;
using Microsoft.AspNetCore.Mvc;
using catto.Models;
using catto.Provider;
using Microsoft.EntityFrameworkCore;

namespace catto.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var user = await _context.Users.Select(u => UserDto.FromUser(u)).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPatch]
    public async Task<ActionResult<UserDto>> Update(UserDto user)
    {
        var userToUpdate = await _userProvider.UpdateUser(user);
        return userToUpdate;
    }


    //TODO
    [HttpPost("login")]
    public async Task<ActionResult<UserDto?>> Login([FromBody] UserDto user)
    {
        var userLogin = await _userProvider.Login(user);

        if (userLogin == null)
        {
            return Unauthorized();
        }

        var token = await _context.Tokens.AddAsync(Token.GenerateToken(userLogin.Id));

        HttpContext.Response.Cookies.Append("Token", token.Entity.TokenKey);

        return userLogin;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto?>> Register(UserDto user)
    {
        var createdUser = await _userProvider.Register(user);

        var token = await _context.Tokens.AddAsync(Token.GenerateToken(user.Id));

        HttpContext.Response.Cookies.Append("Token", token.Entity.TokenKey);

        return createdUser;
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var token = HttpContext.Request.Cookies["Token"];
        HttpContext.Response.Cookies.Delete("Token");

        var tokenToDelete = await _context.Tokens.FirstOrDefaultAsync(t => t.TokenKey == token);
        if (tokenToDelete != null) tokenToDelete.DiscardAt = DateTime.Now;
//send badrequest if token is not found
        await _context.SaveChangesAsync();

        return Ok();
    }
}