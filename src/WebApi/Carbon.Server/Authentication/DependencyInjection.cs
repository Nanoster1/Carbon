using Carbon.Server.Authentication.Configurations;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Carbon.Server.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddCarbonAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options => options.ConfigureAuthentication())
            .AddGoogle(configuration)
            .AddJwtBearer(JwtAuthenticationConfig.AuthenticationScheme, options => options.ConfigureApplicationJwt(configuration));

        return services;
    }

    private static AuthenticationBuilder AddGoogle(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        return builder
            .AddCookie(GoogleAuthenticationConfig.CookieAuthenticationScheme, options => options.ConfigureGoogle())
            .AddOAuth(GoogleAuthenticationConfig.OAuthAuthenticationScheme, options => options.ConfigureGoogle(configuration));
    }
}
