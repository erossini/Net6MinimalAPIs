using MinimalApis.Models;
using System;
using System.Collections.Generic;
using System.Net;
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

        [Fact]
        public async Task PostClients()
        {
            await using var application = new MinimalApisApplication();

            string name = $"Test-{Guid.NewGuid()}";

            var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/clients", new ClientModel { Name = name });
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var clients = await client.GetFromJsonAsync<List<ClientModel>>("/clients");

            var todo = Assert.Single(clients);
            Assert.Equal(name, todo.Name);
        }

        [Fact]
        public async Task DeleteTodos()
        {
            await using var application = new MinimalApisApplication();

            string name = $"Test-{Guid.NewGuid()}";

            var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/clients", new ClientModel { Name = name });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var clients = await client.GetFromJsonAsync<List<ClientModel>>("/clients");

            var clt = Assert.Single(clients);
            Assert.Equal(name, clt.Name);

            response = await client.DeleteAsync($"/clients/{clt.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await client.GetAsync($"/clients/{clt.Id}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}