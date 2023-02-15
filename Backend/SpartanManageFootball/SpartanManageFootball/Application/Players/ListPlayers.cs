using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Players
{
    public class ListPlayers
    {
        public class Query : IRequest<Result<List<Player>>>
        {
        }
        public class Handler : IRequestHandler<Query, Result<List<Player>>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Player>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var players = await _context.Players.ToListAsync();

                return Result<List<Player>>.Success(players);
            }
        }
    }
}
