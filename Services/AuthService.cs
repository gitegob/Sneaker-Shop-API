using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Enums;
using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Services;

public class AuthService
{
    private readonly DataContext dataContext;

    public AuthService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<User> Register(UserRegisterDto userRegisterDto)
    {
        var foundUser = await dataContext.Users.FirstOrDefaultAsync(user => user.Email == userRegisterDto.Email);
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
        dataContext.Users.Add(newUser);
        await dataContext.SaveChangesAsync();
        return newUser;
    }

    public async Task<String> Login(UserLoginDto userLoginDto)
    {
        var foundUser = await dataContext.Users.FirstOrDefaultAsync(user => user.Email == userLoginDto.Email);
        if (foundUser == null) throw new BadHttpRequestException("Invalid Credentials", ((int)HttpStatusCode.BadRequest));
        var isValidPassword = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, foundUser.Password);
        if (!isValidPassword) throw new BadHttpRequestException("Invalid credentials", ((int)HttpStatusCode.BadRequest));
        string jwtToken = CreateToken(foundUser);
        return jwtToken;
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id",user.Id.ToString()),
            new Claim("email",user.Email),
            new Claim(ClaimTypes.Role, user.Role ?? "CLIENT" )
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Hellocomplicatedkey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return jwtToken;
    }

}