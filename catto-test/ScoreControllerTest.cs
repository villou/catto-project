using Xunit;
using System.Collections.Generic;
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
    }
}