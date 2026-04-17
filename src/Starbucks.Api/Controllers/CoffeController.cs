using Core.mediatOR.Contracts;
using Microsoft.AspNetCore.Mvc;
using Starbucks.Application.Coffes.Commands;
using Starbucks.Application.Coffes.DTOs;

namespace Starbucks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<Guid> CreateCoffe(
            CoffeCreateRequest request, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CoffeCreate.Command { CoffeCreateRequest = request },
                cancellationToken);

            return result;
        }
    }
}
