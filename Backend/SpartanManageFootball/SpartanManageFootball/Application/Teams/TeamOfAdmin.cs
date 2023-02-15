using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Teams
{
    public class TeamOfAdmin
    {
        public class Query : IRequest<Squad>
        {
            public string Id { get; set; }
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
                var squad = await _context.Squads.Where(x => x.RegisterUserIdsId == request.Id).FirstOrDefaultAsync();

                return squad;
            }
        }
    }
}
