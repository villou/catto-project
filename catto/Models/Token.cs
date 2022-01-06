namespace catto.Models;

public class Token
{
    public int Id { get; set; }
    public string TokenKey { get; set; }
    
    public DateTime? DiscardAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public Token()
    {
    }
    
    public static Token GenerateToken(int userID)
    {
        return new Token
        {
            TokenKey = Guid.NewGuid().ToString(),
            UserId = userID
        };
    }
    
    
}