using Carbon.Core.Domain.Models.Base;

namespace Carbon.Domain.Users.ValueObjects;

/// <summary>
/// Токен доступа для пользователя
/// </summary>
public sealed record UserAccessToken : ValueObjectWrapper<string, UserAccessToken>
{
    public static UserAccessToken Default => Create(string.Empty);

    [Obsolete(ObsoleteMessage, true)]
    public UserAccessToken() { }
}