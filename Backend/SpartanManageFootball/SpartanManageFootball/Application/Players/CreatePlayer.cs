using FluentValidation;
using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.Players
{
    public class CreatePlayer
    {
        public class PlayerAddCommand : IRequest<Result<Player>>
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public int Number { get; set; }
            public string Position { get; set; }
            public int SuqadTeamId { get; set; }
            public object? SquadTeamId { get; internal set; }

            public class PlayerValidator : AbstractValidator<PlayerAddCommand>
            {
                public PlayerValidator()
                {
                    RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name shouldn't be empty").OverridePropertyName("error");
                    RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name number shouldn't be empty").OverridePropertyName("error");
                    RuleFor(x => x.Age).NotNull().NotEmpty().WithMessage("Age shouldn't be empty").OverridePropertyName("error");
                    RuleFor(x => x.Number).NotNull().NotEmpty().WithMessage("Number shouldn't be empty").OverridePropertyName("error");
                    RuleFor(x => x.Position).NotNull().NotEmpty().WithMessage("Position shouldn't be empty").OverridePropertyName("error");
                }
            }
            public class CommandHandler : IRequestHandler<PlayerAddCommand, Result<Player>>
            {
                private readonly SMFContext _context;

                public CommandHandler(SMFContext context)
                {
                    _context = context;
                }

                public async Task<Result<Player>> Handle(PlayerAddCommand request, CancellationToken cancellationToken)
                {
                    var player = new Player
                    {
                        Name = request.Name,
                        LastName = request.LastName,
                        Age = request.Age,
                        Number = request.Number,
                        Position = request.Position,
                        SquadTeamIdsTeamId = request.SuqadTeamId

                    };

                    await _context.Players.AddAsync(player);

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
}
