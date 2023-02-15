using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Teams
{
    public class ListTeams
    {
        public class Query : IRequest<List<Squad>>
        {
        }
        public class Handler : IRequestHandler<Query, List<Squad>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task<List<Squad>> Handle(Query request, CancellationToken cancellationToken)
            {
                var teams = await _context.Squads.ToListAsync();

                return teams;
            }
        }
    }
}
