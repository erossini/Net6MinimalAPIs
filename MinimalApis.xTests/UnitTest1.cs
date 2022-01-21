using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MinimalApis.Data;
using MinimalApis.Models;
using MinimalApis.xTests;
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
    }
}