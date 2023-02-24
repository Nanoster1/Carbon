using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Carbon.Application.Common.Services.Interfaces;
using Carbon.Application.Common.Services.Models;
using Carbon.Domain.Users;
using Carbon.Infrastructure.Authentication.Settings;

using IdentityModel;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Carbon.Infrastructure.Authentication.Services;

/// <summary>
/// Реализация провайдера Jwt токенов
/// </summary>
public sealed class JwtTokenProvider : IJwtTokenProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenProvider(IOptions<JwtSettings> jwtSettings, IDateTimeProvider dateTimeProvider)
    {
        _jwtSettings = jwtSettings.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(JwtUserData userData)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, userData.Id),
            new Claim(JwtClaimTypes.Name, userData.Username),
            new Claim(JwtClaimTypes.Role, userData.Role)
        };

        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes).UtcDateTime,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public bool IsExpired(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return jwt.ValidTo < _dateTimeProvider.UtcNow;
    }
}