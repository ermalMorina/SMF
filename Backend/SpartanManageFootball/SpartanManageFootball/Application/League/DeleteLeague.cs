using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.League
{
    public class DeleteLeague
    {
        public class DeleteCommand : IRequest<Result<Unit>>
        {
            public int LeagueId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteCommand, Result<Unit>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var league = await _context.Leagues.FindAsync(request.LeagueId);

                _context.Remove(league);

                var success = await _context.SaveChangesAsync() > 0;

                if(success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Something went wrong");
            }
        }
    }
}
