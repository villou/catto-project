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

        Assert.True(user?.Count <= 5);
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

        Assert.Equal("pascal", user?.Username);
        Assert.Equal("motdepasse", user?.Password);

    }

    [Fact]
    public async Task LoginWrongCredentials()
    {
        var client = _fixtures.SetupClient();
        var userDto = new UserDto()
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
        var userDto = new UserDto()
        {
            Username = "martin",
            Password = "matin",
        };
        await using var context = _fixtures.GetContext();
        var userCount = context.Users.Count();

        var response = await client.PostAsJsonAsync("/api/User/register", userDto);
        var user = _fixtures.GetBody<UserDto>(response);

        Assert.Equal("martin", user?.Username);
        Assert.Equal("matin", user?.Password);
        Assert.Equal(context.Users.Count(), userCount + 1);
    }

    [Fact]
    public async Task RegisterUsernameAlreadyExists()
    {
        var client = _fixtures.SetupClient();

        var userDto = new UserDto()
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
}