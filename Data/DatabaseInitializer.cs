using Microsoft.EntityFrameworkCore;

namespace Sneaker_Shop_API.Data;

public static class DatabaseInitializer
{
    public static void Initialize(WebApplication app)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
        using var scope = scopedFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();

        if (app.Environment.IsProduction() || app.Environment.IsStaging())
        {
            dbContext.Database.Migrate();
        }
        else
        {
            // Drop and Recreate database tables
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            //Seed data
            var seedService = scope.ServiceProvider.GetService<DataSeeder>();
            seedService.Seed();
        }
    }
}