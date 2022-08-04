using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Settings;

namespace Sneaker_Shop_API.Services;

public class AuthService
{
    private readonly DataContext _dataContext;
    private readonly AppSettings _appSettings;

    public AuthService(DataContext dataContext,IOptions<AppSettings> appSettings)
    {
        _dataContext = dataContext;
        _appSettings = appSettings.Value;
    }

    public async Task<User> Register(UserRegisterDto userRegisterDto)
    {
        var foundUser = await _dataContext.Users.FirstOrDefaultAsync(user => user.Email == userRegisterDto.Email);
        if (foundUser != null) throw new BadHttpRequestException("User already exists", ((int)HttpStatusCode.Conflict));
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
        if (foundUser == null) throw new BadHttpRequestException("Invalid Credentials", ((int)HttpStatusCode.BadRequest));
        var isValidPassword = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, foundUser.Password);
        if (!isValidPassword) throw new BadHttpRequestException("Invalid credentials", ((int)HttpStatusCode.BadRequest));
        var jwtToken = CreateToken(foundUser);
        return jwtToken;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new("id",user.Id.ToString()),
            new("email",user.Email),
            new(ClaimTypes.Role, user.Role ?? "CLIENT" )
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return jwtToken;
    }

}