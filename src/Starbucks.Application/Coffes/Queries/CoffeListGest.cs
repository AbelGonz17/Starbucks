using Core.Mappy.Interfaces;
using Core.mediatOR.Contracts;
using Microsoft.EntityFrameworkCore;
using Starbucks.Application.Coffes.DTOs;
using Starbucks.Persistence;

namespace Starbucks.Application.Coffes.Queries;

public class CoffeListGest
{
    public class Query : IRequest<List<CoffeResponse>>
    {}

    public class Handler(
        StarcbucksDbContext context, IMapper mapper)
        : IRequestHandler<Query, List<CoffeResponse>>
    {
        private readonly StarcbucksDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<List<CoffeResponse>> Handle(
            Query request,
            CancellationToken cancellationToken)
        {
            var coffes = await _context.Coffes.ToListAsync();
            return _mapper.Map<List<CoffeResponse>>(coffes);
        }
    }
}