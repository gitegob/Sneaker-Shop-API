using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sneaker_Shop_API.Authorization;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Exceptions;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Settings;

namespace Sneaker_Shop_API.Services;

public class AuthService
{
    private readonly DataContext _dataContext;
    private readonly JwtUtils _jwtUtils;

    public AuthService(DataContext dataContext,JwtUtils jwtUtils)
    {
        _dataContext = dataContext;
        _jwtUtils = jwtUtils;
    }

    public async Task<User> Register(UserRegisterDto userRegisterDto)
    {
        var foundUser = await _dataContext.Users.FirstOrDefaultAsync(user => user.Email == userRegisterDto.Email);
        if (foundUser != null) throw new ConflictException("User already exists");
        var newUser = new User
        {
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            Email = userRegisterDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password),
            Phone = userRegisterDto.Phone,
            Address = userRegisterDto.Address,
            Role = userRegisterDto.Role
        };
        _dataContext.Users.Add(newUser);
        await _dataContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<string> Login(UserLoginDto userLoginDto)
    {
        var foundUser = await _dataContext.Users.FirstOrDefaultAsync(user => user.Email == userLoginDto.Email);
        if (foundUser == null) throw new BadRequestException("Invalid Credentials");
        var isValidPassword = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, foundUser.Password);
        if (!isValidPassword) throw new BadRequestException("Invalid credentials");
        var jwtToken = _jwtUtils.GenerateToken(foundUser);
        return jwtToken;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _dataContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

}