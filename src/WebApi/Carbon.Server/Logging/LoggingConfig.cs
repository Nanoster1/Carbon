using Serilog;

namespace Carbon.Server.Logging;

/// <summary>
/// Настройка логирования
/// </summary>
public static class LoggingConfig
{
    public static void ConfigureLogging(HostBuilderContext context, LoggerConfiguration loggerConfig)
    {
        loggerConfig.ReadFrom.Configuration(context.Configuration);
    }
}