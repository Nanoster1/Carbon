namespace Carbon.Application.Common.Services.Models;

/// <summary>
/// Данные о пользователе, используемые в создании токена
/// </summary>
/// <param name="Id">
/// Идентификатор пользователя
/// </param>
/// <param name="Username">
/// Имя пользователя
/// </param>
/// <param name="Role">
/// Роль пользователя
/// </param>
public sealed record JwtUserData(string Id, string Username, string Role);