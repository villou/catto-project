using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using catto.DTO;

namespace catto_test;

public class ScoreControllerTest : IClassFixture<IntegrationFixtures>
{
  private readonly IntegrationFixtures _fixtures;

  public ScoreControllerTest(IntegrationFixtures fixtures)
  {
    _fixtures = fixtures;
  }

  [Fact]
  public async Task GetMaxFiveScoreList()
  {
    var client = _fixtures.SetupClient();
    var response = await client.GetAsync("/api/Score");
    var scores = _fixtures.GetBody<List<ScoreIndexDto>>(response);

    await using var context = _fixtures.GetContext();

    Assert.True(scores?.Count <= 5);
    Assert.True(scores?.First().Username != "");
  }

  [Fact]
  public async Task GetScoreList()
  {
    var client = _fixtures.SetupClient();

    var userDto = new UserDto
    {
      Username = "pascal",
      Password = "motdepasse",
    };
    await client.PostAsJsonAsync("/api/User/login", userDto);

    var scoreDto = new ScoreDto
    {
      Score = 10000
    };
    await using var context = _fixtures.GetContext();
    var scoreCount = context.Scores.Count();
    var response = await client.PostAsJsonAsync("/api/Score", scoreDto);
    response.EnsureSuccessStatusCode();

    var responseScores = await client.GetAsync("/api/Score");
    var scores = _fixtures.GetBody<List<ScoreIndexDto>>(responseScores);

    Assert.Equal(scoreDto.Score, scores?.First().Score);
    Assert.Equal(scoreCount + 1, context.Scores.Count());
  }

  [Fact]
  public async Task GetScoreListWithoutUser()
  {
    var client = _fixtures.SetupClient();

    var scoreDto = new ScoreDto
    {
      Score = 10000
    };
    await using var context = _fixtures.GetContext();
    var scoreCount = context.Scores.Count();
    var response = await client.PostAsJsonAsync("/api/Score", scoreDto);

    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    Assert.Equal(scoreCount, context.Scores.Count());
  }

  [Fact]
  public async Task GetByUserId()
  {
    var client = _fixtures.SetupClient();

    var userDto = new UserDto
    {
      Username = "pascal",
      Password = "motdepasse",
    };
    await client.PostAsJsonAsync("/api/User/login", userDto);

    var response = await client.GetAsync($"/api/Score/{1}");

    var scores = _fixtures.GetBody<List<ScoreDto>>(response);

    await using var context = _fixtures.GetContext();

    Assert.Equal(context.Scores.Count(u => u.UserId == 1), scores?.Count);
  }
}