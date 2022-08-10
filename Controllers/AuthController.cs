using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Services;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService) => (_authService) = (authService);

    [HttpPost("signup")]
    public async Task<ActionResult<ApiResponse<User>>> Register(UserSignupDto userSignupDto)
    {
        var result = await _authService.Signup(userSignupDto);
        return Created(nameof(Register), new ApiResponse("Signup successful", result));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<string>>> Login(UserLoginDto userLoginDto)
    {
        var result = await _authService.Login(userLoginDto);
        return Ok(new ApiResponse("Login successful", result));
    }
}