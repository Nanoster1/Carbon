namespace Carbon.Contracts.Common.Routes.Server;

public static partial class ServerRoutes
{
    public static class Controllers
    {
        public const string ErrorController = $"{Prefix}/Error";
        public const string AuthenticationController = $"{Prefix}/Authentication";
    }
}