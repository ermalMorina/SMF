using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Application.Teams;
using SpartanManageFootball.Models;
using static SpartanManageFootball.Application.Teams.CreateTeams;
using static SpartanManageFootball.Application.Teams.EditTeams;
using static SpartanManageFootball.Application.Teams.VerifyTeams;

namespace SpartanManageFootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SquadController(IMediator mediator)
        {
            _mediator = mediator;
        } 

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost("addSquad")] 
        public async Task<ActionResult<Squad>> CreateTeam([FromForm] TeamCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "agent")]
        [HttpDelete("{id}")] 
        public async Task<ActionResult<Unit>> DeleteTeam(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteTeams.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Squad>>> ListTeam()
        {
            return await Mediator.Send(new ListTeams.Query());
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Squad>> TeamDetails(int id)
        {
            return await Mediator.Send(new TeamsDetails.Query { Id = id });
        }

        //This controller returns the squad of the user who created it
        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<Squad>> AdminTeamDetails(string id)
        {
            var squad = await Mediator.Send(new TeamOfAdmin.Query { Id = id });
            if(squad == null)
            {
                return BadRequest("There is no squad added with this ID");
            }
            return squad;
        }

        //AGENT only verifies the team nothing else
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "agent, admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Squad>> Edit(int id, [FromForm] TeamEditCommand command)
        {
            command.TeamId = id;
            return HandleResult(await Mediator.Send(command));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "agent")]
        [HttpPut("Verify/{squadId}")]
        public async Task<ActionResult<Squad>> VerifyTeams(int squadId, [FromForm] VerifyTeamsCommand command)
        {
            command.TeamId = squadId;
            return await Mediator.Send(command);
        }
    }
}
