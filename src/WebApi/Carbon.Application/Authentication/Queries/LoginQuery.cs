using Carbon.Application.Authentication.Results;
using Carbon.Domain.Users.ValueObjects;

using ErrorOr;

using MediatR;

namespace Carbon.Application.Authentication.Queries;

/// <summary>
/// Запрос на вход пользователя
/// </summary>
public sealed record LoginQuery : IRequest<ErrorOr<AuthenticationResult>>
{
    public LoginQuery(string email, string password)
    {
        Email = UserEmail.Create(email);
        Password = password;
    }

    public UserEmail Email { get; }
    public string Password { get; }
}