using System;
using System.Collections.Generic;
using System.Linq;
using cmsjr.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace cmsjr.TeamService.Tests
{
    public class TeamsControllerTest
    {
        [Fact]
        public async void CreateTeamAddsToList()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var teams = (IEnumerable<Team>)
                (await controller.GetAllTeams() as ObjectResult).Value;
            var original = new List<Team>(teams);

            var newTeam = new Team("sample");
            var result = await controller.CreateTeam(newTeam);
            Assert.Equal((result as ObjectResult).StatusCode, 201);

            var newTeamsRaw = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;

            var newTeams = new List<Team>(newTeamsRaw);

            Assert.Equal(newTeams.Count, original.Count + 1);
            var sampleTeam = newTeams.FirstOrDefault(x => x.Name.Equals("sample"));
            Assert.NotNull(sampleTeam);
        }

        [Fact]
        public async void DeleteNonExistentTeamReturnsNotFound()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var id = Guid.NewGuid();

            var result = await controller.DeleteTeam(id);
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void DeleteTeamRemovesFromList()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var teams = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;
            var ct = teams.Count();

            var sampleName = "sample";
            var id = Guid.NewGuid();
            var sampleTeam = new Team(sampleName, id);
            controller.CreateTeam(sampleTeam);

            teams = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.NotNull(sampleTeam);

            controller.DeleteTeam(id);

            teams = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.Null(sampleTeam);
        }

        [Fact]
        public async void GetNonExistentTeamReturnsNotFound()
        {
            var controller = new TeamsController(new TestTeamRepository());

            var id = Guid.NewGuid();
            var result = await controller.GetTeam(id);
            Assert.True(result is NotFoundObjectResult);
        }

        [Fact]
        public async void GetTeamRetrievesTeam()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var sampleTeamName = "sample";
            var id = Guid.NewGuid();
            var sampleTeam = new Team(sampleTeamName, id);
            controller.CreateTeam(sampleTeam);

            var retrievedTeam = (Team) (await controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, sampleTeamName);
            Assert.Equal(retrievedTeam.ID, id);
        }

        [Fact]
        public async void QueryTeamListReturnsCorrectTeams()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var teamsRaw = (IEnumerable<Team>)
                (await controller.GetAllTeams() as ObjectResult).Value;
            var teams = new List<Team>(teamsRaw);
            Assert.Equal(teams.Count, 2);
        }

        [Fact]
        public async void UpdateNonExistentTeamReturnsNotFound()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var teams = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;
            var original = new List<Team>(teams);

            var someTeam = new Team("Some Team", Guid.NewGuid());
            await controller.CreateTeam(someTeam);

            var newTeamId = Guid.NewGuid();
            var newTeam = new Team("New Team", newTeamId);
            var result = await controller.UpdateTeam(newTeam, newTeamId);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void UpdateTeamModifiesTeamToList()
        {
            var controller = new TeamsController(new TestTeamRepository());
            var teams = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;
            var original = new List<Team>(teams);

            var id = Guid.NewGuid();
            var t = new Team("sample", id);
            var result = controller.CreateTeam(t);

            var newTeam = new Team("sample2", id);
            controller.UpdateTeam(newTeam, id);

            var newTeamsRaw = (IEnumerable<Team>) (await controller.GetAllTeams() as ObjectResult).Value;
            var newTeams = new List<Team>(newTeamsRaw);
            var sampleTeam = newTeams.FirstOrDefault(target => target.Name == "sample");
            Assert.Null(sampleTeam);

            var retrievedTeam = (Team) (await controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, "sample2");
        }
    }
}