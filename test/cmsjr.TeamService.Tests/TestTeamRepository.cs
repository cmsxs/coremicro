using System.Collections.Generic;
using cmsjr.TeamService.Models;
using cmsjr.TeamService.Persistence;

namespace cmsjr.TeamService.Tests
{
    public class TestTeamRepository : MemoryTeamRepository
    {
        public TestTeamRepository() : base(CreateTeams())
        {
            
        }

        private static ICollection<Team> CreateTeams()
        {
            
            var teams = new List<Team>();
            teams.Add(new Team("one"));
            teams.Add(new Team("two"));
            return teams;
        }
        
    }
}