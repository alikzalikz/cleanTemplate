using FluentValidation;

namespace CharchoobApi.Application.Auth.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(req => req.Username)
            .NotEmpty();

        RuleFor(req => req.Password)
            .NotEmpty();
    }
}
