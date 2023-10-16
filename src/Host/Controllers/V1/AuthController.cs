using CharchoobApi.Application.Auth.Commands.Register;
using CharchoobApi.Application.Auth.Queries.Login;
using CharchoobApi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharchoobApi.Host.Controllers.V1;

[ApiVersion("1.0")]
public class AuthController : ApiControllerBase
{
    /// <summary>
    /// register
    /// </summary>
    [HttpPost("register")]
    public async Task<Result> Register([FromBody] RegisterCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// login and get AccessToken and RefreshToken
    /// </summary>
    [HttpPost("login")]
    public async Task<Result<LoginQueryResponse>> Login([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }
}
