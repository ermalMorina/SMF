using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;
using static SpartanManageFootball.Application.Players.CreatePlayer;

namespace SpartanManageFootball.Application.Players
{
    public class EditPlayer
    {
        public class PlayerEditCommand : IRequest<Result<Player>>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public int? Age { get; set; }
            public int? Number { get; set; }
            public string Position { get; set; }
            public int? SquadTeamId { get; set; }
        }
        public class CommandHandler : IRequestHandler<PlayerEditCommand, Result<Player>>
        {
            private readonly SMFContext _context;

            public CommandHandler(SMFContext context)
            {
                _context = context;
            }

            public async Task<Result<Player>> Handle(PlayerEditCommand request, CancellationToken cancellationToken)
            {
                var player = await _context.Players.FindAsync(request.Id);

                if (player == null)
                {
                    return Result<Player>.Failure("There was a problem saving changes");
                }

                player.Name = request.Name ?? player.Name;
                player.LastName = request.LastName ?? player.LastName;
                player.Age = request.Age ?? player.Age;
                player.Number = request.Number ?? player.Number;
                player.Position = request.Position ?? player.Position;
                player.SquadTeamIdsTeamId = request.SquadTeamId ?? player.SquadTeamIdsTeamId;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Result<Player>.Success(player);
                }
                return Result<Player>.Failure("There was a problem saving changes");
            }
        }
    }
}
