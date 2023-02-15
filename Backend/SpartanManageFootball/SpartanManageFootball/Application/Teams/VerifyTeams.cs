using MediatR;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Teams
{
    public class VerifyTeams
    {
        public class VerifyTeamsCommand : IRequest<Squad>
        {
            public int TeamId { get; set; }
            public bool? isVerified { get; set; }
        }
        
        public class CommandHandler : IRequestHandler<VerifyTeamsCommand, Squad>
        {
            private readonly SMFContext _context;

            public CommandHandler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Squad> Handle(VerifyTeamsCommand request, CancellationToken cancellationToken)
            {
                var team = await _context.Squads.FindAsync(request.TeamId);

                if (team == null)
                {
                    throw new Exception("Could not find squad!");
                }

                team.isVerified = request.isVerified ?? team.isVerified;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return team;
                }

                return null;
            }
        }
    }

}
