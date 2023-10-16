using FluentValidation;

namespace CharchoobApi.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(req => req.Username)
            .NotEmpty();

        RuleFor(req => req.Password)
            .NotEmpty();
    }
}
