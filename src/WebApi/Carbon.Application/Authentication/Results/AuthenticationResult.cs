namespace Carbon.Application.Authentication.Results;

/// <summary>
/// Результат аутентификации пользователя
/// </summary>
/// <param name="AccessToken">
/// Токен аутентификации
/// </param>
public sealed record AuthenticationResult(string AccessToken);