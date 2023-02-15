using MediatR;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;
using SpartanManageFootball.Interfaces;
using SpartanManageFootball.Application.Core;

namespace SpartanManageFootball.Application.Teams
{
    public class EditTeams
    {
        public class TeamEditCommand : IRequest<Result<Squad>>
        {
            public int TeamId { get; set; }
            public int? StadiumId { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public bool? isVerified { get; set; }
        }
        public class CommandHandler : IRequestHandler<TeamEditCommand, Result<Squad>>
        {
            private readonly SMFContext _context;
            private readonly IPhotoAccessor _photoAccessor;

            public CommandHandler(SMFContext context, IPhotoAccessor photoAccessor)
            {
                _context = context;
                _photoAccessor = photoAccessor;
            }

            public async Task<Result<Squad>> Handle(TeamEditCommand request, CancellationToken cancellationToken)
            {
                var team = await _context.Squads.FindAsync(request.TeamId);

                if (team == null)
                {
                    return Result<Squad>.Failure("Could not find this squad");
                }

                team.StadiumIdsId = request.StadiumId ?? team.StadiumIdsId;
                team.Name = request.Name ?? team.Name;
                team.City = request.City ?? team.City;
                team.isVerified = request.isVerified ?? team.isVerified;
                
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Squad>.Success(team);
                }

                return Result<Squad>.Failure("Something went wrong");
            }
        }
    }
}
