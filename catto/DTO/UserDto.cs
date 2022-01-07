using catto.Models;

namespace catto.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public static UserDto FromUser(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password
        };
    }
}
