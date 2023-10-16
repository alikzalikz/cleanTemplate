using CharchoobApi.Application.Common.Interfaces;
using CharchoobApi.Application.Common.Models;
using CharchoobApi.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CharchoobApi.Application.Auth.Queries.Login;

public record LoginQuery : IRequest<Result<LoginQueryResponse>>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public record LoginQueryResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}

public class LoginHandler : IRequestHandler<LoginQuery, Result<LoginQueryResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public LoginHandler(UserManager<ApplicationUser> userManager, IIdentityService identityService)
    {
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<Result<LoginQueryResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is not null)
        {
            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (isValidPassword)
            {
                var tokens = await _identityService.GenerateTokensAsync(user);

                var res = new LoginQueryResponse()
                {
                    AccessToken = tokens.AccessToken,
                    RefreshToken = tokens.RefreshToken,
                };

                return Result<LoginQueryResponse>.Succeed(data: res);
            }
        }

        return Result<LoginQueryResponse>.Failure(code: 400, "username or password is wrong");
    }
}
