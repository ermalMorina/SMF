using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.SquadStadium
{
    public class CreateStadium
    {
        public class StadiumCommand : IRequest<Result<Unit>>
        {
            public SpartanManageFootball.Models.Stadium Stadium { get; set; }
        }

        public class StadiumHanlder : IRequestHandler<StadiumCommand, Result<Unit>>
        {
            private readonly SMFContext _context;

            public StadiumHanlder(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(StadiumCommand request, CancellationToken cancellationToken)
            {
                _context.Stadiums.Add(request.Stadium);
                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create the stadium");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
