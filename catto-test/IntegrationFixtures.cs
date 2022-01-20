using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using catto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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

        return CreateClient();
    }

    public CattoContext GetContext()
    {
        return Services.CreateScope().ServiceProvider.GetRequiredService<CattoContext>();
    }
}