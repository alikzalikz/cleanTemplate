using Microsoft.AspNetCore.Mvc;

namespace CharchoobApi.Application.Common.Models;

public class Result
{
    private Result(bool success, int code, string message, Error? errors)
    {
        Success = success;
        Code = code;
        Message = message;
        Errors = errors;
    }

    public bool Success { get; set; }
    public int Code { get; set; }
    public string Message { get; set; }
    public Error? Errors { get; set; }

    public static Result Succeed(int code = 200, string message = "success")
    {
        return new Result(true, code, message, null);
    }

    public static Result Failure(int code, string message = "failure")
    {
        return new Result(false, code, message, null);
    }

    public static Result ValidationError(ValidationProblemDetails? errors)
    {
        var error = Error.ValidationError(errors);

        return new Result(false, 400, "Validation Error", error);
    }

    public static Result ValidationError(List<string> errors)
    {
        var error = new Error()
        {
            ValidationErrors = errors
        };

        return new Result(false, 400, "Validation Error", error);
    }
}
