using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.RefereesOperations
{
    public class CreateReferee
    {
        public class RefereeCommand : IRequest<Result<Referee>>
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Experience { get; set; }
            public string City { get; set; }
            public string Position { get; set; }
            public class CommandHandler : IRequestHandler<RefereeCommand,Result<Referee>>
            {
                private readonly SMFContext _context;

                public CommandHandler(SMFContext context)
                {
                    _context = context;
                }

                public async Task<Result<Referee>> Handle(RefereeCommand request, CancellationToken cancellationToken)
                {
                    var referee = new Referee
                    {
                        Name = request.Name,
                        LastName = request.LastName,
                        Experience = request.Experience,
                        City = request.City,
                        Position = request.Position,
                    };

                    await _context.Referees.AddAsync(referee);

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
}
