using Carbon.Core.AspNetCore.Configuration.Defaults;
using Carbon.Core.AspNetCore.Configuration.Extensions;

namespace Carbon.Server.Configuration;

/// <summary>
/// Конфигурация приложения
/// </summary>
public static class AppConfiguration
{
    /// <summary>
    /// Добавляет конфигурацию приложения
    /// </summary>
    public static IConfigurationBuilder AddAppConfiguration(this IConfigurationBuilder builder, IHostEnvironment environment)
    {
        builder.SetBasePath(ConfigurationDefaults.ConfigurationsDirectory);

        builder.AddFile("app_settings.yaml", false, true, true, environment.EnvironmentName);
        builder.AddFile(Path.Combine("Logging", "logging_settings.yaml"), false, false, true, environment.EnvironmentName);
        builder.AddFile(Path.Combine("Authentication", "authentication_settings.yaml"), false, false, true, environment.EnvironmentName);

        builder.AddEnvironmentVariables();

        if (environment.IsDevelopment())
        {
            builder.AddFile(Path.Combine("Swagger", "swagger_settings.yaml"));
            builder.AddUserSecrets<Program>();
        }

        return builder;
    }
}