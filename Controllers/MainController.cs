using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sneaker_Shop_API.Settings;

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
    private readonly AppSettings _appSettings;

    public MainController(ILogger<MainController> logger, IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpGet(Name = "hello")]
    public ActionResult<string> Get()
    {
        return "Hello";
    }
}
