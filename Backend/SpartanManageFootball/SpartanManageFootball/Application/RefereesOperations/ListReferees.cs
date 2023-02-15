using MediatR;
using Microsoft.EntityFrameworkCore;
using SpartanManageFootball.Application.Core;
using SpartanManageFootball.Models;
using SpartanManageFootball.Persistence;

namespace SpartanManageFootball.Application.RefereesOperations
{
    public class ListReferees
    {
        public class Query : IRequest<Result<List<Referee>>>
        {
        }
        public class Handler : IRequestHandler<Query, Result<List<Referee>>>
        {
            private readonly SMFContext _context;

            public Handler(SMFContext context)
            {
                _context = context;
            }

            public async Task <Result<List<Referee>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var referees = await _context.Referees.ToListAsync();

                return Result<List<Referee>>.Success(referees);
            }
        }
    }
}
