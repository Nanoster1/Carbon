using Carbon.Domain.Users.ValueObjects;

using FluentValidation;

namespace Carbon.Application.Authentication.Commands.Registration;

public sealed class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator(
        IValidator<Username> usernameValidator,
        IValidator<UserEmail> emailValidator)
    {
        RuleFor(x => x.Username).SetValidator(usernameValidator);
        RuleFor(x => x.Email).SetValidator(emailValidator);
        RuleFor(x => x.Password).NotEmpty();
    }
}
