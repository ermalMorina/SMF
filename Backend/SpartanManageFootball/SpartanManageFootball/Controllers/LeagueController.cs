using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Application.League;
using SpartanManageFootball.Application.LeagueSquad;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Models;
using SpartanManageFootball.Services;
using static SpartanManageFootball.Application.LeagueSquad.Add;
using static SpartanManageFootball.Application.League.EditLeague;

namespace SpartanManageFootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : BaseApiController
    {
        private readonly IIdentityService _identityServices;
        private readonly IMediator _mediator;

        public LeagueController
            (
            IIdentityService identityServices,
            IMediator mediator
            )
        {
            _identityServices = identityServices;
            _mediator = mediator;
        }


        [HttpGet("getLeagues")]
        public async Task<ActionResult<List<League>>> GetLeagues()
        {
            return await Mediator.Send(new ListLeague.Query());
        }

        [HttpGet("getLeaguesById")]
        public async Task<ActionResult<League>> GetSquadsInLeagues(int leagueId)
        {
            return Ok(await Mediator.Send(new Get.GetCommand { LeagueId = leagueId }));
        }

        [HttpPost("addLeague")]
        public async Task<IActionResult> CreateLeague(League league)
        {
            return Ok(await Mediator.Send(new CreateLeague.LeagueCommand { League = league }));
        }

        [HttpPost("addSquadsToLeague")]
        public async Task<ActionResult<League>> AddSquadsToLeague(LeagueSquadDto dto)
        {
            return Ok(await Mediator.Send(new Add.Command { dto = dto }));
        }

        [HttpDelete("{leagueId}")]
        public async Task<IActionResult> DeleteLeague(int leagueId)
        {
            return Ok(await Mediator.Send(new DeleteLeague.DeleteCommand { LeagueId = leagueId }));
        }

        [HttpPut("{leagueId}")]
        public async Task<ActionResult<League>> EditLeague(int leagueId, EditLeaugeCommand command)
        {
            command.LeagueId = leagueId;
            return HandleResult(await Mediator.Send(command));
        }

        [HttpPost("UpdatePoints")]
        public async Task<IActionResult> UpdatePoints(List<StandingsDTO> dto,string r)
        {
         
            var result = await _identityServices.AddPointsStandings(dto,r);
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
