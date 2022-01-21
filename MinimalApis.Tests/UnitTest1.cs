using AutoMapper;
using MinimalApis.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [Test]
        public async Task PostTodos()
        {
            await using var application = new MinimalApisApplication();

            string name = $"Test-{Guid.NewGuid()}";

            var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/clients", new ClientModel { Name = name });
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var rsp = await client.GetFromJsonAsync<List<ClientModel>>("/clients");

            var cl = rsp.FirstOrDefault();
            Assert.AreEqual(name, cl.Name);
        }
    }
}