using System;
using System.Collections.Generic;
using System.Linq;
using cmsjr.TeamService.Models;

namespace cmsjr.TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> Teams;

        public MemoryTeamRepository()
        {
            if (Teams == null)
            {
                Teams = new List<Team>();
            }
        }

        public MemoryTeamRepository(ICollection<Team> teams)
        {
            Teams = teams;
        }
        
        public IEnumerable<Team> GetTeams()
        {
            return Teams;
        }

        public Team AddTeam(Team team)
        {
            
            Teams.Add(team);
            return team;
        }

        public Team GetTeam(Guid id)
        {
            return Teams.FirstOrDefault(x => x.ID.Equals(id));
        }

        public Team UpdateTeam(Team team)
        {
            var existingTeam = DeleteTeam(team.ID);
            return existingTeam == null ? null : AddTeam(team);
        }

        public Team DeleteTeam(Guid id)
        {
            var currentTeam = GetTeam(id);
            if (currentTeam != null)
            {
                Teams.Remove(currentTeam);
            }
            return currentTeam;
        }
    }
}