using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Dto;
using Sneaker_Shop_API.Exceptions;
using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Services;

public class UserService
{
    private readonly DataContext _db;

    public UserService(DataContext dataContext)
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
        var filteredSet = _db.Users.Where(s=>s.Id!=null);
        var users = await filteredSet
            .Skip(paginationParams.Size * paginationParams.Page)
            .Take(paginationParams.Size)
            .Select(s => new ViewUserDto(s.Id, s.FirstName, s.LastName, s.Email, s.Role)).ToListAsync();
        return new Page<ViewUserDto>(users, paginationParams.Page, paginationParams.Size,
            filteredSet.Count());
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}