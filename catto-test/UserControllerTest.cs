using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using catto.DTO;
using catto.Models;

namespace catto_test;

public class UserControllerTest : IClassFixture<IntegrationFixtures>
{
    private readonly IntegrationFixtures _fixtures;

    public UserControllerTest(IntegrationFixtures fixtures)
    {
        _fixtures = fixtures;
    }

    [Fact]
    public async Task GetUser()
    {
        var client = _fixtures.SetupClient();
        var response = await client.GetAsync("/api/User");
        var user = _fixtures.GetBody<List<User>>(response);

        await using var context = _fixtures.GetContext();

        Assert.Equal(context.Users.Count(), user?.Count);
    }

    [Fact]
    public async Task Login()
    {
        var client = _fixtures.SetupClient();
        var userDto = new UserDto
        {
            Username = "pascal",
            Password = "motdepasse",
        };

        var response = await client.PostAsJsonAsync("/api/User/login", userDto);
        var user = _fixtures.GetBody<UserDto>(response);

        await using var context = _fixtures.GetContext();

        Assert.Equal(userDto.Username, user?.Username);
        Assert.Equal(userDto.Password, user?.Password);

    }

    [Fact]
    public async Task LoginWrongCredentials()
    {
        var client = _fixtures.SetupClient();
        var userDto = new UserDto
        {
            Username = "pascal",
            Password = "123456",
        };

        var response = await client.PostAsJsonAsync("/api/User/login", userDto);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

    }

    [Fact]
    public async Task Register()
    {
        var client = _fixtures.SetupClient();
        var userDto = new UserDto
        {
            Username = "martin",
            Password = "matin",
        };
        await using var context = _fixtures.GetContext();
        var userCount = context.Users.Count();

        var response = await client.PostAsJsonAsync("/api/User/register", userDto);
        var user = _fixtures.GetBody<UserDto>(response);

        Assert.Equal(userDto.Username, user?.Username);
        Assert.Equal(userDto.Password, user?.Password);
        Assert.Equal(context.Users.Count(), userCount + 1);
    }

    [Fact]
    public async Task RegisterUsernameAlreadyExists()
    {
        var client = _fixtures.SetupClient();

        var userDto = new UserDto
        {
            Username = "pascal",
            Password = "zerozero",
        };
        await using var context = _fixtures.GetContext();
        var userCount = context.Users.Count();
        var response = await client.PostAsJsonAsync("/api/User/register", userDto);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal(context.Users.Count(), userCount);
    }
    
    [Fact]
    public async Task GetUserInexistantId()
    {
        var client = _fixtures.SetupClient();
        var response = await client.GetAsync("/api/User/26");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateUser()
    {
        var client = _fixtures.SetupClient();
        var userDto = new UserDto
        {
            Password = "newpassword",
        };
        var response = await client.PutAsJsonAsync("/api/User/1", userDto);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Logout()
    {
        var client = _fixtures.SetupClient();
        var userDto = new UserDto
        {
            Username = "pascal",
            Password = "motdepasse",
        };
        await client.PostAsJsonAsync("/api/User/login", userDto);
        var response = await client.PostAsJsonAsync("/api/User/logout", "");
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task Delete()
    {
        var client = _fixtures.SetupClient();
        await using var context = _fixtures.GetContext();
        var userCount = context.Users.Count();

        var response = await client.DeleteAsync("api/User/1");
        response.EnsureSuccessStatusCode();

        Assert.Equal(context.Users.Count(), userCount - 1);
    }
}