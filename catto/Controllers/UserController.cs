﻿using catto.DTO;
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
    return user == null ? Unauthorized() : Ok(user);
  }

  [HttpGet]
  public List<User> Get()
  {
    return _context.Users.ToList();
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<UserDto>> GetUserById(int id)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    if (user == null)
    {
      return NotFound("User not found");
    }

    return Ok(UserDto.FromUser(user));

  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UserDto user)
  {
    var userToUpdate = await _userProvider.UpdateUser(id, user);

    if (userToUpdate == null)
    {
      return BadRequest("Can't update user");
    }

    await _context.SaveChangesAsync();
    return Ok(userToUpdate);
  }


  [HttpPost("login")]
  public async Task<ActionResult<UserDto?>> Login([FromBody] UserDto user)
  {
    var userLogin = await _userProvider.Login(user);

    if (userLogin == null)
    {
      return Unauthorized("Invalid credentials");
    }

    var token = await _context.Tokens.AddAsync(Token.GenerateToken(userLogin.Id));

    HttpContext.Response.Cookies.Append("Token", token.Entity.TokenKey);

    await _context.SaveChangesAsync();

    return Created("", userLogin);
  }

  [HttpPost("register")]
  public async Task<ActionResult<UserDto?>> Register(UserDto user)
  {
    var createdUser = await _userProvider.Register(user);

    if (createdUser == null)
    {
      return Conflict("User already exists");
    }

    var token = await _context.Tokens.AddAsync(Token.GenerateToken(createdUser.Id));

    HttpContext.Response.Cookies.Append("Token", token.Entity.TokenKey);

    await _context.SaveChangesAsync();

    return Created("", createdUser);
  }

  [HttpPost("logout")]
  public async Task<ActionResult> Logout()
  {
    var token = HttpContext.Request.Cookies["Token"];
    HttpContext.Response.Cookies.Delete("Token");

    var tokenToDelete = await _context.Tokens.FirstOrDefaultAsync(t => t.TokenKey == token);
    if (tokenToDelete != null) tokenToDelete.DiscardAt = DateTime.Now;

    if (token == null)
    {
      return BadRequest("No token found");
    }
    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> Delete(int id)
  {
    var userToDelete = await _userProvider.DeleteUser(id);

    if (userToDelete == null)
    {
      return BadRequest("Can't delete user");
    }

    await _context.SaveChangesAsync();
    return NoContent();

  }
}