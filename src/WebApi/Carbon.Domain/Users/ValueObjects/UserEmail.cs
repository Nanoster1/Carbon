using System.Text.RegularExpressions;

using Carbon.Core.Domain.Exceptions;
using Carbon.Core.Domain.Models.Base;

namespace Carbon.Domain.Users.ValueObjects;

/// <summary>
/// Email пользователя
/// </summary>
public sealed record UserEmail : ValueObjectWrapper<string, UserEmail>
{
    public static Regex Pattern { get; } = new(".+@.+\\..+", RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

    [Obsolete(ObsoleteMessage, true)]
    public UserEmail() { }

    protected override void OnValidateAndThrow()
    {
        if (!Pattern.IsMatch(Value))
            throw new ValueObjectIsNotValidException(nameof(Value));
    }
}
