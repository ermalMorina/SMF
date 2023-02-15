using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.SquadStadium
{
    public class EditStadium
    {
        public class EditStadiumCommand : IRequest<Result<SpartanManageFootball.Models.Stadium>>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public int Capacity { get; set; }
        }

        public class CommandHanlder : IRequestHandler<EditStadiumCommand, Result<SpartanManageFootball.Models.Stadium>>
        {
            private readonly SMFContext _context;
            public CommandHanlder(SMFContext context)
            {
                _context = context;
            }
            public async Task<Result<Models.Stadium>> Handle(EditStadiumCommand request, CancellationToken cancellationToken)
            {
                var stadium = await _context.Stadiums.FindAsync(request.Id);

                stadium.Name = request.Name;
                stadium.Location = request.Location;
                stadium.Capacity = request.Capacity;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<SpartanManageFootball.Models.Stadium>.Success(stadium);
                }

                return Result<SpartanManageFootball.Models.Stadium>.Failure("Something went wrong");
            }
        }
    }
}
