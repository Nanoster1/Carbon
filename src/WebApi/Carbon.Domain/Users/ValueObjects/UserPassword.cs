using Carbon.Core.Domain.Exceptions;
using Carbon.Core.Domain.Models.Base;

namespace Carbon.Domain.Users.ValueObjects;

/// <summary>
/// Данные о пароле пользователя
/// </summary>
/// <value></value>
public sealed record UserPassword : ValueObject
{
    private UserPassword(string hash, byte[] salt)
    {
        Hash = hash;
        Salt = salt;
    }

    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string Hash { get; private set; }
    /// <summary>
    /// Соль, используемая для хеширования пароля
    /// </summary>
    public byte[] Salt { get; private set; }

    public static UserPassword Create(string hash, byte[] salt)
    {
        return new UserPassword(hash, salt);
    }

    protected override void OnValidateAndThrow()
    {
        if (string.IsNullOrWhiteSpace(Hash))
            throw new ValueObjectIsNotValidException(nameof(Hash), "Hash must be not null or empty");

        if (Salt is not { Length: > 0 })
            throw new ValueObjectIsNotValidException(nameof(Salt), "Salt must be not empty");
    }
}