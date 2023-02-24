namespace Carbon.Contracts.Authentication.Responses;

/// <summary>
/// Ответ на запрос входа пользователя
/// </summary>
/// <param name="AccessToken">
/// Токен доступа
/// </param>
public sealed record AuthenticationResponse(string AccessToken);