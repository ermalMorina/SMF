using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Teams
{
    public class DeleteTeams
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Command,Result<Unit>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var squad = await _context.Squads.FindAsync(request.Id);

                if (squad == null)
                {
                    return Result<Unit>.Failure("This squad doesnt exist");
                }

                _context.Remove(squad);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Something went wrong");
            }
        }
    }
}
