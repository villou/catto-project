﻿using catto.DTO;
using catto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catto.Provider;
public class UserProvider
{
  private readonly CattoContext _context;

  public UserProvider(CattoContext context)
  {
    _context = context;
  }

  public async Task<UserDto?> Register(UserDto userDto)
  {
    {
      var user = new User()
      {
        Id = userDto.Id,
        Username = userDto.Username,
        Password = userDto.Password,

      };

      var userExists = await _context.Users.AnyAsync(u => u.Username == user.Username);

      if (userExists)
      {
        return null;
      }


      var createdUser = await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      return UserDto.FromUser(createdUser.Entity);
    }
  }

  public async Task<User?> GetUserFromToken(HttpContext httpContext)
  {
    var token = httpContext.Request.Cookies["Token"];
    if (token == null)
    {
      return null;
    }
    var userFromToken = await _context.Tokens
        .Include(t => t.User)
        .FirstOrDefaultAsync(t => t.TokenKey == token);
    return userFromToken?.User;
  }

  public async Task<UserDto?> Login(UserDto userDto)
  {
    var userLogin = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username && u.Password == userDto.Password);

    if (userLogin == null)
    {
      return null;
    }

    return UserDto.FromUser(userLogin);
  }


  public async Task<UserDto?> UpdateUser(int userId, UserDto userDto)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
      return null;
    }

    if (userDto.Password != "")
    {
      user.Password = userDto.Password;
    }

    if (userDto.Avatar != "")
    {
      user.Avatar = userDto.Avatar;
    }

    return UserDto.FromUser(user);
  }

  public async Task<UserDto?> DeleteUser(int userId)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
      return null;
    }

    var token = await _context.Tokens.Where(t => t.UserId == user.Id).ToListAsync();
    if (token.Count > 0)
    {
      _context.Tokens.RemoveRange(token);
    }
    
    _context.Users.Remove(user);
    return UserDto.FromUser(user);
  }

}