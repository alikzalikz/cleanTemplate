using Swashbuckle.AspNetCore.Filters;

namespace CharchoobApi.Application.Auth.Queries.Login;

public class LoginQueryExample : IExamplesProvider<LoginQuery>
{
    public LoginQuery GetExamples()
    {
        return new LoginQuery()
        {
            Username = "Test",
            Password = "Pa$$w0rd"
        };
    }
}
