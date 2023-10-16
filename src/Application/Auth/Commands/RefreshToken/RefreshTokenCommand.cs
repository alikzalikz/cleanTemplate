using CharchoobApi.Application.Common.Interfaces;
using CharchoobApi.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CharchoobApi.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<Result<RefreshTokenCommandResponse>>
{
    public required string ExpiredAccessToken { get; init; }
    public required string RefreshToken { get; init; }
}

public record RefreshTokenCommandResponse
{
    public required string NewAccessToken { get; set; }
    public required string NewRefreshToken { get; set; }
}

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenCommandResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IIdentityService _identityService;

    public RefreshTokenHandler(IApplicationDbContext dbContext, IIdentityService identityService)
    {
        _dbContext = dbContext;
        _identityService = identityService;
    }

    public async Task<Result<RefreshTokenCommandResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.ExpiredAccessToken)
            && !string.IsNullOrEmpty(request.RefreshToken))
        {
            var storedToken = await _dbContext.TblRefreshToken
                .FirstOrDefaultAsync(record => record.Token == request.RefreshToken, cancellationToken);

            string? jti = _identityService.DecodeJti(request.ExpiredAccessToken);

            if (jti != null
                && storedToken != null
                && storedToken.JwtId == jti
                && storedToken.ExpiryDate >= DateTime.UtcNow
                && !storedToken.IsUsed
                && !storedToken.IsRevoked)
            {
                var tokens = await _identityService
                    .RefreshTokenAsync(request.ExpiredAccessToken, storedToken);

                if (tokens is not null)
                {
                    var res = new RefreshTokenCommandResponse()
                    {
                        NewAccessToken = tokens.AccessToken,
                        NewRefreshToken = tokens.RefreshToken,
                    };

                    return Result<RefreshTokenCommandResponse>.Succeed(data: res);
                }
            }
        }

        return Result<RefreshTokenCommandResponse>.Failure();
    }
}
