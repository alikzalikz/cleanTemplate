using Swashbuckle.AspNetCore.Filters;

namespace CharchoobApi.Application.Auth.Commands.RefreshToken;

public class RefreshTokenCommandExample : IExamplesProvider<RefreshTokenCommand>
{
    public RefreshTokenCommand GetExamples()
    {
        return new RefreshTokenCommand()
        {
            ExpiredAccessToken = "ENTER HERE EXPIRED ACCESS TOKEN",
            RefreshToken = "ENTER HERE REFRESH TOKEN"
        };
    }
}
