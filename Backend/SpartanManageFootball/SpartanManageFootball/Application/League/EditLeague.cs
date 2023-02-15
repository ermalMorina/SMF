using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.League
{
    public class EditLeague
    {
        public class EditLeaugeCommand : IRequest<Result<SpartanManageFootball.Models.League>>
        {
            public int LeagueId { get; set; }
            public string LeagueName { get; set; }
        }

        public class CommandHandler : IRequestHandler<EditLeaugeCommand, Result<SpartanManageFootball.Models.League>>
        {
            private readonly SMFContext _context;

            public CommandHandler(SMFContext context)
            {
                _context = context;
            }
            public async Task<Result<Models.League>> Handle(EditLeaugeCommand request, CancellationToken cancellationToken)
            {
                var league = await _context.Leagues.FindAsync(request.LeagueId);

                league.LeagueName = request.LeagueName;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<SpartanManageFootball.Models.League>.Success(league);
                }

                return Result<SpartanManageFootball.Models.League>.Failure("Something went wrong");
            }
        }
    }
}
