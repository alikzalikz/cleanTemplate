using CharchoobApi.Application.Common.Interfaces;
using CharchoobApi.Application.Common.Models;
using CharchoobApi.Domain.Common;
using MediatR;

namespace CharchoobApi.Application.Auth.Commands.Register;

public record RegisterCommand : IRequest<Result>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class RegisterHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RegisterHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isCreate = await _identityService
            .CreateUserAsync(request.Username, request.Email, request.Password);

        if (isCreate)
        {
            return Result.Succeed();
        }

        return Result.Failure(code: 500, message: Sd.ServerError);
    }
}
