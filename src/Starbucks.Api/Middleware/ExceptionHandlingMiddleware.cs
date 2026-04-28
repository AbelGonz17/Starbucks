using Starbucks.Application.Abstractions;

namespace Starbucks.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Este error es una excepcion");

            if(e is Application.Execptions.ValidationException validationEx)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(validationEx.Errors));
                return;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            var error = new Error(
                "UnexpectedError",
                _env.IsDevelopment() ? e.ToString() : "A ocurrido un error inessperado"
            );

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
        }
    }
}