using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Services;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService authService;
    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(UserRegisterDto userRegisterDto)
    {
        var result = await authService.Register(userRegisterDto);
        return Created(nameof(Register), result);
    }
    [HttpPost("login")]
    public async Task<ActionResult<String>> Login(UserLoginDto userLoginDto)
    {
        var result = await authService.Login(userLoginDto);
        return Ok(result);
    }
}