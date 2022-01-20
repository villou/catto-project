using catto.DTO;
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

    
    public async Task<UserDto?> UpdateUser(UserDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);

        if (user == null) 
        {
           return null; 
        }

        if (userDto.Password != null)
        {
            user.Password = userDto.Password;
        }

        if (userDto.Avatar != null)
        {
            user.Avatar = userDto.Avatar;
        }
        
        return UserDto.FromUser(user);
    }

    public async Task<UserDto?> DeleteUser(UserDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);

        if (user == null) 
        {
           return null; 
        }

        var token = await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == user.Id);
        if (token == null)
        {
            return null;
        }
        
        _context.Tokens.Remove(token);
        _context.Users.Remove(user);


        return UserDto.FromUser(user);
    }

}