namespace Carbon.Application.Common.Services.Models;

/// <summary>
/// Хэшированный пароль
/// </summary>
/// <param name="Hash">
/// Хэш пароля
/// </param>
/// <param name="Salt">
/// Соль пароля
/// </param>
public sealed record HashedPassword(string Hash, byte[] Salt);