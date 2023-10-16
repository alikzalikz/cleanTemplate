using Microsoft.AspNetCore.Mvc;

namespace CharchoobApi.Application.Common.Models;


public class Error
{
    public List<string>? ValidationErrors { get; set; }

    public static Error ValidationError(ValidationProblemDetails? errors)
    {
        var validationErrors = new List<string>();

        foreach (var key in errors!.Errors)
        {
            foreach (var value in key.Value)
            {
                validationErrors.Add(value);
            }
        }

        return new Error()
        {
            ValidationErrors = validationErrors
        };
    }
}
