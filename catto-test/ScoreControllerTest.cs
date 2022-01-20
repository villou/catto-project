using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using catto.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace catto_test;

public class ScoreControllerTest : IClassFixture<IntegrationFixtures>
{
    private readonly IntegrationFixtures _fixtures;

    public ScoreControllerTest(IntegrationFixtures fixtures)
    {
        _fixtures = fixtures;
    }
    
    [Fact]
    public async Task Test1()
    {
        var client = _fixtures.SetupClient();
        var response = await client.GetAsync("/api/Score");
        var scores = System.Text.Json.JsonSerializer.Deserialize<List<Score>>(response.Content.ReadAsStringAsync().Result); 
    }
}