using System.Net.Http.Headers;
using System.Text.Json;

using Carbon.Core.AspNetCore.Configuration.Exceptions;
using Carbon.Server.Authentication.Defaults;
using Carbon.Server.Authentication.Settings;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Carbon.Server.Authentication.Configurations;

public static class GoogleAuthenticationConfig
{
    public const string OAuthAuthenticationScheme = "GoogleOAuth";
    public const string CookieAuthenticationScheme = "GoogleCookie";
    public const string CallbackPath = "/authentication-code/callback";

    public static void ConfigureGoogle(this OAuthOptions options, IConfiguration configuration)
    {
        var oauthSettings = configuration.GetSection(GoogleAuthenticationSettings.SectionName).Get<GoogleAuthenticationSettings>() ??
            throw new ConfigSettingsNotFoundException(GoogleAuthenticationSettings.SectionName);

        options.SignInScheme = CookieAuthenticationScheme;
        options.ClientId = oauthSettings.ClientId;
        options.ClientSecret = oauthSettings.ClientSecret;
        options.CorrelationCookie.SameSite = SameSiteMode.Lax;
        options.AuthorizationEndpoint = oauthSettings.AuthorizationEndpoint;
        options.TokenEndpoint = oauthSettings.TokenEndpoint;
        options.CallbackPath = new PathString(CallbackPath);
        options.UserInformationEndpoint = oauthSettings.UserInformationEndpoint;
        foreach (var scope in oauthSettings.Scopes) options.Scope.Add(scope);
        options.ClaimActions.MapJsonKey(GoogleClaimTypes.Id, oauthSettings.IdJsonKey);
        options.ClaimActions.MapJsonKey(GoogleClaimTypes.Name, oauthSettings.NameJsonKey);
        options.ClaimActions.MapJsonKey(GoogleClaimTypes.Email, oauthSettings.EmailJsonKey);
        options.Events.OnCreatingTicket += async context =>
        {
            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();
            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            context.RunClaimActions(json.RootElement);
        };
    }

    public static void ConfigureGoogle(this CookieAuthenticationOptions options)
    {
        options.ExpireTimeSpan = TimeSpan.Zero;
    }
}
