using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using catto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace catto_test;

public class IntegrationFixtures : WebApplicationFactory<Program>
{
    private readonly List<User> _users = new();
    private readonly List<Score> _scores = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Environment", "Test");
    }

    public HttpClient SetupClient()
    {
        using (var context = GetContext())
        {
            context.Database.EnsureDeleted();
        }
        SetupFixtures();
        return CreateClient();
    }

    public CattoContext GetContext()
    {
        return Services.CreateScope().ServiceProvider.GetRequiredService<CattoContext>();
    }

    private void SetupFixtures()
    {
        using var context = GetContext();
        var user = new User
        {
            Username = "pascal",
            Password = "motdepasse",
            Avatar = "https://imageserver.petsbest.com/marketing/blog/cat-behavior-issues.jpg",
        };
        context.Users.Add(user);
        context.SaveChanges();

        var score = new Score
        {
            Id = 1,
            UserId = user.Id,
            Value = 10,
        };
        context.Scores.Add(score);
        context.SaveChanges();
    }

    public T? GetBody<T>(HttpResponseMessage response)
    {
        return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
    }
}