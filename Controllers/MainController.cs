using Microsoft.AspNetCore.Mvc;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/")]
public class MainController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "hello")]
    public ActionResult<String> Get()
    {
        return "Hello";
    }
}
