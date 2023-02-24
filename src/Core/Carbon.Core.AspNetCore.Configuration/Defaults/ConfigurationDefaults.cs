namespace Carbon.Core.AspNetCore.Configuration.Defaults;

/// <summary>
/// Класс со стандартными значениями для конфигураций
/// </summary>
public static class ConfigurationDefaults
{
    /// <summary>
    /// Путь к директории с конфигурациями
    /// </summary>
    public static readonly string ConfigurationsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Configurations");
}