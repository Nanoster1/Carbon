using System.Text;

using Carbon.Core.AspNetCore.Configuration.Exceptions;
using Carbon.Infrastructure.Authentication.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Carbon.Server.Authentication.Configurations;

public static class JwtAuthenticationConfig
{
    public const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;

    public static void ConfigureApplicationJwt(this JwtBearerOptions options, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ??
            throw new ConfigSettingsNotFoundException(JwtSettings.SectionName);

        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    }
}