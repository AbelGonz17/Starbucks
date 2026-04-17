using Core.Mappy.Interfaces;
using Core.mediatOR.Contracts;
using Starbucks.Application.Coffes.DTOs;
using Starbucks.Domain;
using Starbucks.Persistence;

namespace Starbucks.Application.Coffes.Commands;

public class CoffeCreate
{
    public class Command : IRequest<Guid>
    {
        public required CoffeCreateRequest CoffeCreateRequest { get; set; }
    }

    public class Handler(StarcbucksDbContext context, IMapper mapper) : IRequestHandler<Command, Guid>
    {
        private readonly StarcbucksDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<Guid> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var coffe = _mapper.Map<Coffe>(request.CoffeCreateRequest);
            _context.Add(coffe);
            await _context.SaveChangesAsync(cancellationToken);
            
            return coffe.Id;
        }
    }
}