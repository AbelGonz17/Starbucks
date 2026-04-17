using Core.Mappy.Interfaces;
using Core.mediatOR.Contracts;
using Microsoft.EntityFrameworkCore;
using Starbucks.Application.Categories.DTOs;
using Starbucks.Persistence;

namespace Starbucks.Application.Categories.Queries;

public class CategoryListGet
{
    public class Query : IRequest<List<CategoryResponse>>
    {}

    public class Handler(StarcbucksDbContext context, IMapper mapper) 
    : IRequestHandler<Query, List<CategoryResponse>>
    {
        private readonly StarcbucksDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<List<CategoryResponse>> Handle(
            Query request, 
            CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
    }
}