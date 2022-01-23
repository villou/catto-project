using System.Linq;
using catto.Models;
using Xunit;

namespace catto_test;

public class TokenTest
{
 
    [Fact]
    public void TestToken()
    {
        var userId = 1;
        var token = Token.GenerateToken(userId);
        Assert.Equal(userId, token.UserId);
        Assert.True(token.TokenKey.Count() > 10);
    }
}