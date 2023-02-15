using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Interfaces;

namespace SpartanManageFootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : BaseApiController
    {
        private readonly IIdentityService _identityService;
        public MatchesController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GenerateMatches(int id)
        {
            var a = await _identityService.GenerateGames(id);
            return Ok(a);
        }

        [HttpGet("GetMatches")]
        public async Task<IActionResult> GetMatches()
        {
            var result = await _identityService.GetMatches();
            return Ok(result);
        }

        [HttpGet("GetStandings")]
        public async Task<IActionResult> GetStandings()
        {
            var result = await _identityService.GetStandingsTable();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost("InitializeStandings")]
        public async Task<IActionResult> InitializeStandings(int leagueid)
        {
            var result = await _identityService.DefaultPointsStandings(leagueid);
            if (result != null)
            {
                return Ok("Succeded");
            }
            return BadRequest("Something went wrong");
        }
    }
}
