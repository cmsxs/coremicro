using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using cmsjr.TeamService.Models;
using cmsjr.TeamService.Persistence;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace cmsjr.TeamService
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(IEnumerable<Team>), "Teams returned")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(IActionResult), "Malformed Request")]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, typeof(IActionResult), "Internal Server Error")]
        public virtual async Task<IActionResult> GetAllTeams()
        {
            return Ok(_teamRepository.GetTeams());
        }

        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(Team), "Team Created")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(IActionResult), "Malformed Request")]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, typeof(IActionResult), "Internal Server Error")]
        public virtual async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            _teamRepository.AddTeam(team);
            return Created($"/teams/{team.ID}", team);
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(Team), "Team Retrieved")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(IActionResult), "Malformed Request")]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, typeof(IActionResult), "Internal Server Error")]
        public virtual async Task<IActionResult> GetTeam(Guid id)
        {
            var team = _teamRepository.GetTeam(id);
            return team == null ? (IActionResult) NotFound(id) : Ok(team);
        }
        
        [HttpPut("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(Team), "Team Updated")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(IActionResult), "Malformed Request")]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, typeof(IActionResult), "Internal Server Error")]
        public virtual async Task<IActionResult> UpdateTeam([FromBody] Team team, Guid id)
        {
            team.ID = id;
            if (_teamRepository.UpdateTeam(team) == null)
            {
                return NotFound();
            }
            return Ok(team);
        }
        [HttpDelete("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, typeof(Team), "Team Updated")]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, typeof(IActionResult), "Malformed Request")]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, typeof(IActionResult), "Internal Server Error")]
        public virtual async Task<IActionResult> DeleteTeam( Guid id)
        {
            var team = _teamRepository.DeleteTeam(id);
            return team == null ? (IActionResult) NotFound() : Ok(team.ID);
        }
    }
}