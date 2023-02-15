using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.League
{
    public class ListLeague
    {
        public class Query : IRequest<List<SpartanManageFootball.Models.League>>
        {

        }
        public class Handler : IRequestHandler<Query, List<SpartanManageFootball.Models.League>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }
            public async Task<List<SpartanManageFootball.Models.League>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Leagues.ToListAsync(cancellationToken);
            }
        }
    }
}
