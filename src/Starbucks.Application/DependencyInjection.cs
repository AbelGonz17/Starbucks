using Core.Mappy.Extensions;
using Core.mediatOR;
using Microsoft.Extensions.DependencyInjection;

namespace Starbucks.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(
        this IServiceCollection services
    )
    {
        services.AddMediatOR(typeof(DependencyInjection).Assembly);

        services.AddMapper();

        return services;
    }
}