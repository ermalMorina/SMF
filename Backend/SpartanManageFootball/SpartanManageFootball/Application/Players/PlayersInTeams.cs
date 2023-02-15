using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Players
{
    public class PlayersInTeams
    {
        public class GetCommand : IRequest<List<Player>>
        {
            public int SquadTeamId { get; set; }
        }
        public class GetHandler : IRequestHandler<GetCommand, List<Player>>
        {
            private readonly IIdentityService _identityService;

            public GetHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }
            public async Task<List<Player>> Handle(GetCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.GetPlayersOfSquad(request.SquadTeamId);
                return result;
            }
        }
    }
} 