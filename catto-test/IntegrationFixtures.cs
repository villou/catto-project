using System.Net.Http;
using catto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace catto_test;

public class IntegrationFixtures : WebApplicationFactory<Program>
{
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

    var user2 = new User
    {
      Username = "poisson",
      Password = "passedemot",
      Avatar = "https://imageserver.petsbest.com/marketing/blog/cat-behavior-issues.jpg",
    };
    context.Users.AddRange(user, user2);
    context.SaveChanges();


    var score1 = new Score
    {
      Id = 1,
      UserId = user.Id,
      Value = 10,
    };

    var score2 = new Score
    {
      Id = 2,
      UserId = user.Id,
      Value = 10,
    };

    var score3 = new Score
    {
      Id = 3,
      UserId = user.Id,
      Value = 10,
    };

    var score4 = new Score
    {
      Id = 4,
      UserId = user.Id,
      Value = 10,
    };

    var score5 = new Score
    {
      Id = 5,
      UserId = user.Id,
      Value = 10,
    };

    context.Scores.AddRange(score1, score2, score3, score4, score5);
    context.SaveChanges();
  }

  public T? GetBody<T>(HttpResponseMessage response)
  {
    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
  }
}