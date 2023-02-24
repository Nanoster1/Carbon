namespace Carbon.Contracts.Common.Routes.Server;

/// <summary>
/// Константы для маршрутов сервера
/// </summary>
public static partial class ServerRoutes
{
    /// <summary>
    /// Префикс для всех маршрутов
    /// </summary>
    public const string Prefix = "/api";
    public const string HealthCheck = $"{Prefix}/health";
}