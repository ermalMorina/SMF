using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpartanManageFootball.Interfaces;

namespace SpartanManageFootball.Application.LeagueSquad
{
    public class Get
    {
        public class GetCommand : IRequest<List<SpartanManageFootball.Models.League>>
        {
            public int LeagueId { get; set; }
        }
        public class GetHandler : IRequestHandler<GetCommand, List<SpartanManageFootball.Models.League>>
        {
            private readonly IIdentityService _identityService;

            public GetHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }
            public async Task<List<SpartanManageFootball.Models.League>> Handle(GetCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.GetSquadsInLeagues(request.LeagueId);
                return result;
            }
        }
    }
}
