using FluentValidation;

namespace CharchoobApi.Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(req => req.Username)
            .NotEmpty();

        RuleFor(req => req.Password)
            .NotEmpty();

        RuleFor(req => req.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} can not be empty")
            .EmailAddress()
            .WithMessage("email is not in the correct format");
    }
}
