using Carbon.Application.Authentication.Results;
using Carbon.Domain.Users.ValueObjects;

using ErrorOr;

using MediatR;

namespace Carbon.Application.Authentication.Commands;

/// <summary>
/// Команда регистрации пользователя
/// </summary>
public sealed record RegistrationCommand : IRequest<ErrorOr<AuthenticationResult>>
{
    /// <param name="username">
    /// Имя пользователя
    /// </param>
    /// <param name="email">
    /// Email пользователя
    /// </param>
    /// <param name="password">
    /// Пароль пользователя
    /// </param>
    public RegistrationCommand(string username, string email, string password)
    {
        Username = Username.Create(username);
        Email = UserEmail.Create(email);
        Password = password;
    }

    public Username Username { get; }
    public UserEmail Email { get; }
    public string Password { get; }
}