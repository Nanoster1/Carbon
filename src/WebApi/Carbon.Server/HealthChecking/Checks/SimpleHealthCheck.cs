using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Carbon.Server.HealthChecking.Checks;

public class SimpleHealthCheck : IHealthCheck
{
    private ILogger<SimpleHealthCheck> _logger;

    public SimpleHealthCheck(ILogger<SimpleHealthCheck> logger)
    {
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking health...");
        _logger.LogInformation("Healthy");
        return Task.FromResult(HealthCheckResult.Healthy("Healthy"));
    }
}