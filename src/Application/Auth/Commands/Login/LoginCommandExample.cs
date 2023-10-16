using Swashbuckle.AspNetCore.Filters;

namespace CharchoobApi.Application.Auth.Commands.Login;

public class LoginCommandExample : IExamplesProvider<LoginCommand>
{
    public LoginCommand GetExamples()
    {
        return new LoginCommand()
        {
            Username = "Test",
            Password = "Pa$$w0rd"
        };
    }
}
