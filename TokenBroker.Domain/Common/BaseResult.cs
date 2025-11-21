namespace TokenBroker.Domain.Common;

public class BaseResult<T>
{
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public T? Data { get; private set; }

    private BaseResult(bool success, T? data = default, string? message = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static BaseResult<T> Ok(T data) => new(true, data);
    public static BaseResult<T> Fail(string message) => new(false, default, message);
}