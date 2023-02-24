using Carbon.Core.Domain.Models.Base;

namespace Carbon.Domain.Users.ValueObjects;

/// <summary>
/// Идентификатор пользователя
/// </summary>
public sealed record UserId : ValueObjectWrapper<Guid, UserId>
{
    [Obsolete(ObsoleteMessage, true)]
    public UserId() { }
}