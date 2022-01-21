using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MinimalApis.Data;
using MinimalApis.Models;
using MinimalApis.xTests;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MinimalApis.xTests
{
    public class UnitTest1
    {

        [Fact]
        public async Task GetClients()
        {
            await using var application = new MinimalApisApplication();

            var client = application.CreateClient();
            var notes = await client.GetFromJsonAsync<List<ClientModel>>("/clients");

            Assert.Empty(notes);
        }
    }

    class MinimalApisApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ClientContext>));

                services.AddDbContext<ClientContext>(options =>
                    options.UseInMemoryDatabase("Testing", root));
            });

            return base.CreateHost(builder);
        }
    }
}