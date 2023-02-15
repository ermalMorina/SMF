using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Players
{
    public class PlayerDetails
    {
        public class Query : IRequest<Result<Player>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Player>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task <Result<Player>> Handle(Query request, CancellationToken cancellationToken)
            {
                var player = await _context.Players.FindAsync(request.Id);

                return Result<Player>.Success(player);
            }
        }
    }
}
