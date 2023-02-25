using System.Text.RegularExpressions;

using Carbon.Domain.Users.ValueObjects;

using FluentValidation;

namespace Carbon.Application.Common.DomainValidators.Users;

public sealed class UserEmailValidator : AbstractValidator<UserEmail>
{
    public UserEmailValidator()
    {
        RuleFor(x => x.Value)
            .Matches(UserEmail.Pattern)
            .OverridePropertyName(string.Empty);
    }
}
