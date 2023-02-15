using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.RefereesOperations
{
    public class DeleteReferees
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
                var referee = await _context.Referees.FindAsync(request.Id);

                if (referee == null)
                {
                    return Result<Unit>.Failure("Referee does not exist");
                }

                _context.Remove(referee);

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
