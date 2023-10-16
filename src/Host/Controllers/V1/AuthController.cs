using CharchoobApi.Application.Auth.Commands.Register;
using CharchoobApi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace CharchoobApi.Host.Controllers.V1;

[ApiVersion("1.0")]
public class AuthController : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<Result> Register([FromBody] RegisterCommand command)
    {
        return await Mediator.Send(command);
    }
}
