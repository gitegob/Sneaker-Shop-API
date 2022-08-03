using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Services;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(UserRegisterDto userRegisterDto)
    {
        var result = await _authService.Register(userRegisterDto);
        return Created(nameof(Register), result);
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
    {
        var result = await _authService.Login(userLoginDto);
        return Ok(result);
    }
}