using CharchoobApi.Application.Common.Interfaces;
using CharchoobApi.Application.Common.Models;
using CharchoobApi.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CharchoobApi.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<Result<LoginCommandResponse>>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public record LoginCommandResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}

public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public LoginHandler(UserManager<ApplicationUser> userManager, IIdentityService identityService)
    {
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is not null)
        {
            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (isValidPassword)
            {
                var tokens = await _identityService.GenerateTokensAsync(user);

                var res = new LoginCommandResponse()
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                };

                return Result<LoginCommandResponse>.Succeed(data: res);
            }
        }

        return Result<LoginCommandResponse>.Failure(code: 400, "username or password is wrong");
    }
}
