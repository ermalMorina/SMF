using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Players
{
    public class DeletePlayer
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
                var player = await _context.Players.FindAsync(request.Id);

                if (player == null)
                {
                    return Result<Unit>.Failure("Could not find this player with this id");
                }

                _context.Remove(player);

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
