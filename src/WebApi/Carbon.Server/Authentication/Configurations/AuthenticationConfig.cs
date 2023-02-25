using Microsoft.AspNetCore.Authentication;

namespace Carbon.Server.Authentication.Configurations;

public static class AuthenticationConfig
{
    public static void ConfigureAuthentication(this AuthenticationOptions options)
    {
        options.DefaultScheme = JwtAuthenticationConfig.AuthenticationScheme;
    }
}
