using Carbon.Application.Common.Services.Models;

namespace Carbon.Application.Common.Services.Interfaces;

/// <summary>
/// Интерфейс для получения хеша пароля пользователя
/// </summary>
public interface IPasswordHashProvider
{
    /// <summary>
    /// Генерирует хеш пароля с использованием указанной соли
    /// </summary>
    /// <param name="password">
    /// Пароль пользователя
    /// </param>
    /// <param name="salt">
    /// Соль пароля
    /// </param>
    /// <returns>
    /// Хеш пароля пользователя и его соль
    /// </returns>
    HashedPassword GetHash(string password, byte[] salt);
    /// <summary>
    /// Генерирует новый хеш пароля
    /// </summary>
    /// <param name="password">
    /// Пароль пользователя
    /// </param>
    HashedPassword GetHash(string password);
}