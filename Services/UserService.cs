using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Exceptions;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Utils;

namespace Sneaker_Shop_API.Services;

public class UserService
{
    private readonly DatabaseContext _db;

    public UserService(DatabaseContext dataContext)
    {
        _db = dataContext;
    }

    public async Task<User> RegisterUser(CreateUserDto userRegisterDto)
    {
        var foundUser = await GetByEmail(userRegisterDto.Email);
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
        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();
        return newUser;
    }

    public async Task<Page<ViewUserDto>> GetUsers(PaginationParams paginationParams)
    {
        var query = _db.Users.Where(s => !s.IsDeleted).OrderByDescending(s => s.CreatedAt)
            .Select(u => new ViewUserDto(u.Id, u.FirstName, u.LastName,u.Email,u.Role));
        var result = await PaginationUtil.Paginate(query, paginationParams.Page, paginationParams.Size);
        return result;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(user => !user.IsDeleted && user.Email.Equals(email));
    }
}