using MediatR;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Teams
{
    public class TeamsDetails
    {
        public class Query : IRequest<Squad>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Squad>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Squad> Handle(Query request, CancellationToken cancellationToken)
            {
                var team = await _context.Squads.FindAsync(request.Id);

                return team;
            }
        }
    }
}
