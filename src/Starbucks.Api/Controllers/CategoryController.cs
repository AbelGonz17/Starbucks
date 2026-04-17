using Core.mediatOR.Contracts;
using Microsoft.AspNetCore.Mvc;
using Starbucks.Application.Categories.DTOs;
using static Starbucks.Application.Categories.Queries.CategoryListGet;

namespace Starbucks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<List<CategoryResponse>> Get(CancellationToken cancellationToken)
    {
        var query = new Query();
        var resultados = await _mediator.Send(query, cancellationToken);
        return resultados;
    }
}