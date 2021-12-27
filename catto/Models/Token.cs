namespace catto.Models;

public class Token
{
    public int Id { get; set; }
    public string TokenKey { get; set; }
    
    public DateTime? DiscardAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual List<User> Users { get; set; }
    
    public Token(string tokenKey)
    {
        TokenKey = tokenKey;
        CreatedAt = DateTime.Now;
    }
}