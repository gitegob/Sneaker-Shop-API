using Bogus;
using Microsoft.Extensions.Options;
using Sneaker_Shop_API.Enums;
using Sneaker_Shop_API.Models;
using Sneaker_Shop_API.Settings;
using Sneaker_Shop_API.Utils;

namespace Sneaker_Shop_API.Data;

public class DataSeeder
{
    private readonly DatabaseContext _dbContext;
    private readonly AppSettings _appSettings;
    private Faker _faker = new();

    public DataSeeder(DatabaseContext dbContext, IOptions<AppSettings> appSettings) =>
        (_dbContext, _appSettings) = (dbContext, appSettings.Value);

    public void Seed()
    {
        SeedUsers();
        SeedSneakers();
    }

    private void SeedUsers()
    {
        if (_dbContext.Users.Any()) return;
        var users = new List<User>()
        {
            new()
            {
                FirstName = "Admin",
                LastName = "Dotnet",
                Email = "admin@dotnet.rw",
                Password = PasswordEncryption.HashPassword(_appSettings.DefaultPassword),
                Phone = "+250785721391",
                Address = "Kigali",
                Role = ERoles.ADMIN,
            }
        };

        _dbContext.Users.AddRange(users);
        _dbContext.SaveChanges();
    }

    private void SeedSneakers()
    {
        if (_dbContext.Sneakers.Any()) return;
        var sneakers = Enumerable.Range(1, 30).Select(i => new Sneaker
        {
            Model = _faker.Commerce.ProductName(),
            Colors = new[] { _faker.Commerce.Color(), _faker.Commerce.Color() },
            Price = double.Parse(_faker.Commerce.Price()),
        }).ToList();
        _dbContext.Sneakers.AddRange(sneakers);
        _dbContext.SaveChanges();
    }
}