using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Services;

namespace Sneaker_Shop_API.Controllers;

[ApiController]
[Route("/api/v1/users")]
[Authorize]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ApiResponse<User>>> CreateUser(CreateUserDto userRegisterDto)
    {
        var result = await _userService.RegisterUser(userRegisterDto);
        return Created(nameof(CreateUser),new ApiResponse("Registration successful", result));
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiResponse<Page<ViewUserDto>>>> GetUsers([FromQuery] PaginationParams paginationParams)
    {
        var result = await _userService.GetUsers(paginationParams);
        return Ok(new ApiResponse("Users retrieved", result));
    }
}