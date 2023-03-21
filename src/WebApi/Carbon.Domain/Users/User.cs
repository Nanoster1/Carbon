using Carbon.Core.Domain.Models.Base;
using Carbon.Domain.Users.Enums;
using Carbon.Domain.Users.ValueObjects;

using ErrorOr;

namespace Carbon.Domain.Users;

/// <summary>
/// Пользователь системы
/// </summary>
public sealed class User : AggregateRoot<UserId>
{
    private User(Username username, UserEmail email, UserRole role)
    {
        Username = username;
        Email = email;
        Role = role;
        Password = null!;
    }

    /// <summary>
    /// Фабричный метод для создания пользователя
    /// </summary>
    public static ErrorOr<User> Create(Username username, UserEmail email, UserPassword password)
    {
        ValidateAndThrow(username, email, password);
        return new User(username, email, UserRole.User)
        {
            Password = password
        };
    }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public Username Username { get; private set; }
    /// <summary>
    /// Email пользователя
    /// </summary>
    public UserEmail Email { get; private set; }
    /// <summary>
    /// Данные пароля пользователя
    /// </summary>
    public UserPassword Password { get; private set; }
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; private set; }
    /// <summary>
    /// Токен доступа пользователя
    /// </summary>
    public UserAccessToken AccessToken { get; private set; } = MakeDefault<UserAccessToken>();

    public ErrorOr<Updated> ChangeAccessToken(UserAccessToken accessToken)
    {
        ValidateAndThrow(accessToken);
        AccessToken = accessToken;
        return Result.Updated;
    }
}
