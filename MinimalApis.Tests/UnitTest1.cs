using MinimalApis.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MinimalApis.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetClients()
        {
            await using var application = new MinimalApisApplication();

            var client = application.CreateClient();
            var notes = await client.GetFromJsonAsync<List<ClientModel>>("/clients");

            Assert.IsNotNull(notes);
            Assert.IsTrue(notes.Count == 0);
        }
    }
}