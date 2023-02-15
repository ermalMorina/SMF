using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.SquadStadium
{
    public class GetStadium
    {
        public class Query : IRequest<List<SpartanManageFootball.Models.Stadium>>
        {

        }

        public class GetHandler : IRequestHandler<Query, List<SpartanManageFootball.Models.Stadium>>
        {
            private readonly SMFContext _context;

            public GetHandler(SMFContext context)
            {
                _context = context;
            }
            public async Task<List<Models.Stadium>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Stadiums.ToListAsync(cancellationToken);
            }
        }
    }
}
