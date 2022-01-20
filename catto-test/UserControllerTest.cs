using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using catto.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace catto_test;

public class UserControllerTest : IClassFixture<IntegrationFixtures>
{
    private readonly IntegrationFixtures _fixtures;

    public UserControllerTest(IntegrationFixtures fixtures)
    {
        _fixtures = fixtures;
    }
    
    [Fact]
    public async Task GetUserList()
    {
    }
}