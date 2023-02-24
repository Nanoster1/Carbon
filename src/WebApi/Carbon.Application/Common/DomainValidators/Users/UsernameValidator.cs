using Carbon.Domain.Users.ValueObjects;

using FluentValidation;

namespace Carbon.Application.Common.DomainValidators.Users;

public sealed class UsernameValidator : AbstractValidator<Username>
{
    public UsernameValidator()
    {
        RuleFor(x => x.Value)
            .Matches(Username.Pattern)
            .MaximumLength(Username.MaxLength)
            .MinimumLength(Username.MinLength)
            .OverridePropertyName(string.Empty);
    }
}