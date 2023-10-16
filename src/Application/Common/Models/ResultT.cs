namespace CharchoobApi.Application.Common.Models;

public class Result<T>
{
    private Result(bool success, int code, string message, T? data = default)
    {
        Data = data;
        Success = success;
        Code = code;
        Message = message;
        Errors = null;
    }

    public bool Success { get; set; }
    public int Code { get; set; }
    public string Message { get; set; }
    public Error? Errors { get; set; }
    public T? Data { get; set; }

    public static Result<T> Succeed(T data, int code = 200, string message = "success")
    {
        return new Result<T>(true, code, message, data);
    }

    public static Result<T> Failure(int code = 400, string message = "failure")
    {
        return new Result<T>(false, code, message); 
    }
}
