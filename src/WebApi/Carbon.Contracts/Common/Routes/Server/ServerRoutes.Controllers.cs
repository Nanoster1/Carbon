namespace Carbon.Contracts.Common.Routes.Server;

public static partial class ServerRoutes
{
    /// <summary>
    /// Маршруты контроллеров
    /// </summary>
    public static class Controllers
    {
        public const string ErrorController = $"{Prefix}/Error";
        public const string AuthenticationController = $"{Prefix}/Authentication";
        public const string UserController = $"{Prefix}/Users";
    }
}
