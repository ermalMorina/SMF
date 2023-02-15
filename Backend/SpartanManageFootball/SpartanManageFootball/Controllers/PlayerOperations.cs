using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Application.Players;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Models;
using static SpartanManageFootball.Application.Players.CreatePlayer;
using static SpartanManageFootball.Application.Players.EditPlayer;

namespace SpartanManageFootball.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PlayerOperations : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        public PlayerOperations(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPost("addPlayer")]
        public async Task<ActionResult<Player>> CreatePlayer([FromBody] PlayerAddCommand command)
        {
            return HandleResult(await _mediator.Send(command));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeletePlayer(int id)
        {
            return HandleResult(await _mediator.Send(new DeletePlayer.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Player>>> List()
        {
            return HandleResult(await _mediator.Send(new ListPlayers.Query()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> PlayerDetails(int id)
        {
            return HandleResult(await _mediator.Send(new PlayerDetails.Query { Id = id }));
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        public async Task<ActionResult<List<Player>>> GetPlayersOfSquad(int id)
        {
            return await _mediator.Send(new PlayersInTeams.GetCommand { SquadTeamId = id});
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Player>> Edit(int id, PlayerEditCommand command)
        {
            command.Id = id;
            return HandleResult(await _mediator.Send(command));
        }
    }
}