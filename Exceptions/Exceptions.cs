namespace Sneaker_Shop_API.Exceptions;

[Serializable]
public class HttpException : Exception
{
    public HttpException()
    {
    }

    public HttpException(int statusCode, string? message = "Something went wrong", object? value = null) :
        base(message) =>
        (StatusCode, Value) = (statusCode, value);

    public int StatusCode { get; }
    public object? Value { get; }
}

public class BadRequestException : HttpException
{
    public BadRequestException(string? message = "Bad request",object? value = null) : base(400, message,value)
    {
    }
}
public class NotFoundException : HttpException
{
    public NotFoundException(string? message = "Resource Not Found",object? value = null) : base(404, message,value)
    {
    }
}
public class ConflictException : HttpException
{
    public ConflictException(string? message = "Conflict",object? value = null) : base(409, message,value)
    {
    }
}
public class ForbiddenException : HttpException
{
    public ForbiddenException(string? message = "Forbidden resource",object? value = null) : base(403, message,value)
    {
    }
}