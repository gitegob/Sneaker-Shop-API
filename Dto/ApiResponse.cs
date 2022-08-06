namespace Sneaker_Shop_API.Dto;
[Serializable]
public class ApiResponse<T>
{
    public ApiResponse()
    {
    }

    public ApiResponse(string message, T? payload = default) => (Message, Payload) = (message, payload);
    public string Message { get; set; } = string.Empty;
    public T? Payload { get; set; } = default;
}