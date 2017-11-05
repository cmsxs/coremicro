using System;
using System.Collections.Generic;
using cmsjr.TeamService.Models;

namespace cmsjr.TeamService.Persistence
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeams();
        Team AddTeam(Team team);
        Team GetTeam(Guid id);
        Team UpdateTeam(Team team);
        Team DeleteTeam(Guid id);
    }
}