using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Application.SquadStadium;
using SpartanManageFootball.Models;
using static SpartanManageFootball.Application.SquadStadium.EditStadium;

namespace SpartanManageFootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : BaseApiController
    {
        [HttpPost("create-stadium")]
        public async Task<IActionResult> CreateStadium(Stadium stadium)
        {
            return Ok(await Mediator.Send(new CreateStadium.StadiumCommand { Stadium = stadium }));
        }

        [HttpGet("get-stadium")]
        public async Task<ActionResult<List<Stadium>>> GetStadium()
        {
            return await Mediator.Send(new GetStadium.Query());
        }

        [HttpDelete("{stadiumId}")]
        public async Task<IActionResult> DeleteStadium(int stadiumId)
        {
            return Ok(await Mediator.Send(new DeleteStadium.DeleteCommand { StadiumId = stadiumId}));
        }

        [HttpPut("{stadiumId}")]
        public async Task<ActionResult<Stadium>> EditStadium(int stadiumId, EditStadiumCommand command)
        {
            command.Id = stadiumId;
            return HandleResult(await Mediator.Send(command));
        }
    }
}
