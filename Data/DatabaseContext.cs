using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Models;

namespace Sneaker_Shop_API.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<User>? Users { get; set; }
    public DbSet<Sneaker>? Sneakers { get; set; }
}