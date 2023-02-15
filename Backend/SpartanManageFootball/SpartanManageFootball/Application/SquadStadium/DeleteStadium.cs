using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.SquadStadium
{
    public class DeleteStadium
    {
        public class DeleteCommand : IRequest<Result<Unit>>
        {
            public int StadiumId { get; set; }
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
                var stadium = await _context.Stadiums.FindAsync(request.StadiumId);

                if (stadium == null)
                {
                    return Result<Unit>.Failure("Could not find the stadium with this id.");
                }

                _context.Remove(stadium);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Unit>.Success(Unit.Value);
                }

                return Result<Unit>.Failure("Something failed when trying to add the stadium.");
            }
        }
    }
}
