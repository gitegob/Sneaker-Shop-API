using Sneaker_Shop_API.Authorization;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Exceptions;
using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Services;

public class AuthService
{
    private readonly DataContext _dataContext;
    private readonly JwtUtils _jwtUtils;
    private readonly UserService _userService;

    public AuthService(DataContext dataContext, JwtUtils jwtUtils, UserService userService)
    {
        _dataContext = dataContext;
        _jwtUtils = jwtUtils;
        _userService = userService;
    }

    public async Task<User> Signup(UserSignupDto userSignupDto)
    {
        var (firstName, lastName, email, password, phone, address) = userSignupDto;
        return await _userService.RegisterUser(new CreateUserDto(firstName, lastName, email, password, phone, address,
            "CLIENT"));
    }

    public async Task<string> Login(UserLoginDto userLoginDto)
    {
        var foundUser = await _userService.GetByEmail(userLoginDto.Email);
        if (foundUser == null) throw new BadRequestException("Invalid Credentials");
        var isValidPassword = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, foundUser.Password);
        if (!isValidPassword) throw new BadRequestException("Invalid credentials");
        var jwtToken = _jwtUtils.GenerateToken(foundUser);
        return jwtToken;
    }

}