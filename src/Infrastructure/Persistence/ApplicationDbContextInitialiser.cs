using EngTech.Domain.Entities.Products;
using EngTech.Domain.Entities.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EngTech.Infrastructure.Persistence;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        ApplicationDbContextInitialiser initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;


    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Products.Any())
        {
            _context.Products.Add(new Product
            {
                Title = "کالا 1",
                Discount = 0,
                InventoryCount = 5,
                Price = 10000,
                FinalPrice = 10000
            });
            _context.Products.Add(new Product
            {
                Title = "کالا 2",
                Discount = 1,
                InventoryCount = 5,
                Price = 15000,
                FinalPrice = 13500
            });


            await _context.SaveChangesAsync();
        }


        if (!_context.Users.Any())
        {
            _context.Users.Add(new User { Name = "کاربر1" });
            _context.Users.Add(new User { Name = "کاربر2" });
            _context.Users.Add(new User { Name = "کاربر3" });

            await _context.SaveChangesAsync();
        }
    }
}
