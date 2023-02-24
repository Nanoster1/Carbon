using Carbon.Application.Common.Services.Models;
using Carbon.Domain.Users;

namespace Carbon.Application.Common.Services.Interfaces;

/// <summary>
/// Интерфейс для взаимодействия с jwt токенами
/// </summary>
public interface IJwtTokenProvider
{
    /// <summary>
    /// Сгенерировать jwt токен
    /// </summary>
    /// <param name="userData">
    /// Данные пользователь для которого будет сгенерирован jwt токен
    /// </param>
    string GenerateToken(JwtUserData userData);
    /// <summary>
    /// Проверяет токен на срок действия
    /// </summary>
    /// <param name="token">
    /// Токен для проверки
    /// </param>
    bool IsExpired(string token);
}