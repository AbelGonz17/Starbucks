using Core.mediatOR.Contracts;
using Core.MediatOR.Contracts;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace Starbucks.Application.Abstractions;

public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LogginBehavior<TRequest, TResponse>> _logger;

    public LogginBehavior(
        ILogger<LogginBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation(
            "Empezando la operacion consulta de {RequestName}", 
            typeof(TRequest).Name);

        var response = await next();

        _logger.LogInformation(
            "Terminando la operacion consulta de {RequestName}", 
            typeof(TRequest).Name);

        return response;
    }
}
