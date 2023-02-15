using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Application.RefereesOperations;
using SpartanManageFootball.Models;
using static SpartanManageFootball.Application.RefereesOperations.CreateReferee;
using static SpartanManageFootball.Application.RefereesOperations.EditReferee;

namespace SpartanManageFootball.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefereesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RefereesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "agent")]
        [HttpPost("addReferee")]
        public async Task<IActionResult> CreateReferee([FromBody] RefereeCommand command)
        {
            return HandleResult(await _mediator.Send(command));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "agent")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteReferee(int id)
        {
            return HandleResult(await _mediator.Send(new DeleteReferees.Command { Id = id }));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Referee>>> List()
        {
            return HandleResult(await _mediator.Send(new ListReferees.Query()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Referee>> RefereeDetails(int id)
        {
            return HandleResult(await _mediator.Send(new RefereeDetails.Query { Id = id }));
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "agent")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Referee>> Edit(int id, RefereeEditCommand command)
        {
            command.Id = id;

            return HandleResult(await _mediator.Send(command));
        }
    }
}
