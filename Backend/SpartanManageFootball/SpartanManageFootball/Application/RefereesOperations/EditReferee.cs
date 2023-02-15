using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.RefereesOperations
{
    public class EditReferee
    {
        public class RefereeEditCommand : IRequest<Result<Referee>>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Experience { get; set; }
            public string City { get; set; }
            public string Position { get; set; }
        }
        public class CommandHandler : IRequestHandler<RefereeEditCommand, Result<Referee>>
        {
            private readonly SMFContext _context;

            public CommandHandler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<Referee>> Handle(RefereeEditCommand request, CancellationToken cancellationToken)
            {
                var referee = await _context.Referees.FindAsync(request.Id);
                if (referee == null)
                {
                    Result<Referee>.Failure("Referee does not exist");
                }

                referee.Name = request.Name ?? referee.Name;
                referee.LastName = request.LastName ?? referee.LastName;
                referee.Experience = request.Experience ?? referee.Experience;
                referee.City = request.City ?? referee.City;
                referee.Position = request.Position ?? referee.Position;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Referee>.Success(referee);
                }

                return Result<Referee>.Failure("There was a problem saving changes");
            }
        }
    }
}
