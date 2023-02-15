using MediatR;
using SpartanManageFootball.DTOs;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Models;

namespace SpartanManageFootball.Application.LeagueSquad
{
    public class Add
    {
        public class Command : IRequest<Unit>
        {
            public LeagueSquadDto dto { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly IIdentityService _identityService;

            public CommandHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _identityService.AddSquadsToLeague(request.dto);
                return result;
            }
        }
    }
}
