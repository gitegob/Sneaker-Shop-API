namespace Sneaker_Shop_API.Dto;

[Serializable]
public class ApiResponse
{
    public ApiResponse()
    {
    }

    public ApiResponse(string message, object? payload = default) => (Message, Payload) = (message, payload);
    public string Message { get; set; } = string.Empty;
    public object? Payload { get; set; }
}

[Serializable]
public class ApiResponse<T> : ApiResponse
{
    public ApiResponse()
    {
    }

    public ApiResponse(string message, T? payload = default) => (Message, Payload) = (message, payload);
    public string Message { get; set; } = string.Empty;
    public T? Payload { get; set; } = default;
}