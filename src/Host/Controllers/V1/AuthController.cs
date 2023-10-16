using System.Threading;
using CharchoobApi.Application.Auth.Commands.Login;
using CharchoobApi.Application.Auth.Commands.RefreshToken;
using CharchoobApi.Application.Auth.Commands.Register;
using CharchoobApi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
    public async Task<Result<LoginCommandResponse>> Login([FromBody] LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// get new AccessToken and RefreshToken
    /// </summary>
    [HttpPost("refresh-token")]
    public async Task<ActionResult<Result<RefreshTokenCommandResponse>>> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var res = await Mediator.Send(command);

        if (res.Success)
        {
            return Ok(res);
        }

        return Forbid();
    }
}
