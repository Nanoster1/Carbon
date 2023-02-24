using System.Text.RegularExpressions;

using Carbon.Core.Domain.Exceptions;
using Carbon.Core.Domain.Models.Base;

namespace Carbon.Domain.Users.ValueObjects;

/// <summary>
/// Имя пользователя
/// </summary>
public sealed record Username : ValueObjectWrapper<string, Username>
{
    public const int MaxLength = 15;
    public const int MinLength = 3;
    public static Regex Pattern { get; } = new(".+", RegexOptions.Compiled);

    [Obsolete(ObsoleteMessage, true)]
    public Username() { }

    protected override void OnValidateAndThrow()
    {
        if (!Pattern.IsMatch(Value))
            throw new ValueObjectIsNotValidException(nameof(Value));

        if (Value is { Length: > MaxLength or < MinLength })
            throw new ValueObjectIsNotValidException(nameof(Value), $"{nameof(Value)} length must be between {MinLength} and {MaxLength}");
    }
}