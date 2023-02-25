using Carbon.Infrastructure.Persistance;
using Carbon.Server.HealthChecking.Checks;

namespace Carbon.Server.HealthChecking;

public static class DependencyInjection
{
    public static IServiceCollection AddCarbonHealthChecking(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SimpleHealthCheck>(nameof(SimpleHealthCheck))
            .AddDbContextCheck<CarbonDbContext>();

        return services;
    }
}
