using Carbon.Domain.Users.ValueObjects;

using FluentValidation;

namespace Carbon.Application.Authentication.Queries.Login;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator(IValidator<UserEmail> userEmailValidator)
    {
        RuleFor(x => x.Email)
            .SetValidator(userEmailValidator);

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
