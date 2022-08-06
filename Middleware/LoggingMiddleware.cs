namespace Sneaker_Shop_API.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var startTime = DateTime.Now;
        _logger.LogInformation($"==============Starting Request ({context.Request.Method} {context.Request.Path}) at {startTime}=============");
        await _next(context);
        var endTime = DateTime.Now;
        var duration = endTime  - startTime;
        _logger.LogInformation($"==============End Request ({context.Request.Method} {context.Request.Path}) at {endTime} in {duration.Milliseconds} ms===========");
    }
}