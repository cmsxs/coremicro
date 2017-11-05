using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using cmsjr.TeamService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace cmsjr.TeamService.Tests.Integration
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _testClient;

        private readonly Team _teamZombie;

        public SimpleIntegrationTests()
        {
            _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            _testClient = _testServer.CreateClient();
            _teamZombie = new Team()
            {
                ID = Guid.NewGuid(),
                Name = "Zombie"
            };
            
             
            
        }
        
        [Fact]
        public async void TestTeamPostAndGet()
        {
            StringContent stringContent = new StringContent(            
                JsonConvert.SerializeObject(_teamZombie),
                UnicodeEncoding.UTF8,
                "application/json");

            HttpResponseMessage postResponse = 
                await _testClient.PostAsync(
                    "api/v1/teams",
                    stringContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await _testClient.GetAsync("api/v1/teams");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();            
            List<Team> teams = 
                JsonConvert.DeserializeObject<List<Team>>(raw);
            Assert.Equal(1, teams.Count());
            Assert.Equal("Zombie", teams[0].Name);
            Assert.Equal(_teamZombie.ID, teams[0].ID);
        }
    }
}