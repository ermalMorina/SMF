using MediatR;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.RefereesOperations
{
    public class RefereeDetails
    {
        public class Query : IRequest<Result<Referee>>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Referee>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task <Result<Referee>> Handle(Query request, CancellationToken cancellationToken)
            {
                var referee = await _context.Referees.FindAsync(request.Id);

                return Result<Referee>.Success(referee);
            }
        }
    }
}
