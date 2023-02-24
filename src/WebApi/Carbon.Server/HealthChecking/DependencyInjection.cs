using Carbon.Infrastructure.Persistance;
using Carbon.Server.HealthChecking.Checks;

namespace Carbon.Server.HealthChecking;

public static class DependencyInjection
{
    public static void AddCarbonHealthChecking(this IServiceCollection services)
    {
        var builder = services.AddHealthChecks();
        builder.AddCheck<SimpleHealthCheck>(nameof(SimpleHealthCheck));
        builder.AddDbContextCheck<CarbonDbContext>();
    }
}