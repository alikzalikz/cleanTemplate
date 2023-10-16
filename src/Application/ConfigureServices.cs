using System.Reflection;
using CharchoobApi.Application.Common.Models;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation(option =>
        {
            option.OverrideDefaultResultFactoryWith<ValidationResult>();
        });
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
