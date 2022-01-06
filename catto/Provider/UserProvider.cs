using catto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserProvider
{
    private readonly CattoContext _context;

    public UserProvider(CattoContext context)
    {
        _context = context;
    }

    public async Task<User> Register(User user)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Username == user.Username);
        if (userExists)
        {
            return null;
        }
        
        var createdUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        // created something need to return a 201
        return createdUser.Entity;
    }

    public async Task<User?> GetUserFromToken(HttpContext httpContext)
    {
        var token = httpContext.Request.Cookies["Token"];
        if (token == null)
        {
            return null;
        }
        var user = await _context.Tokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.TokenKey == token);
        return user?.User;
    }
}