using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.League
{
    public class CreateLeague
    {
        public class LeagueCommand : IRequest<Result<Unit>>
        {
            public SpartanManageFootball.Models.League League { get; set; }
        }

        public class Handler : IRequestHandler<LeagueCommand, Result<Unit>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(LeagueCommand request, CancellationToken cancellationToken)
            {
                _context.Leagues.Add(request.League);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create the league");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
