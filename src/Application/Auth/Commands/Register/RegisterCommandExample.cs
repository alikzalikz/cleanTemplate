using Swashbuckle.AspNetCore.Filters;

namespace CharchoobApi.Application.Auth.Commands.Register;

public class RegisterCommandExample : IExamplesProvider<RegisterCommand>
{
    public RegisterCommand GetExamples()
    {
        return new RegisterCommand()
        {
            Username = "Test",
            Email = "test@test.com",
            Password = "Pa$$w0rd"
        };
    }
}
