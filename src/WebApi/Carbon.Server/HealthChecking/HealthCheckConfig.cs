using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Carbon.Server.HealthChecking;

public static class HealthCheckConfig
{
    public static HealthCheckOptions Options => new HealthCheckOptions()
    {
        AllowCachingResponses = false,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    };
}