using Microsoft.EntityFrameworkCore;

namespace Lab2Server.Configuration.Database;

public static class DatabaseSeederExtensions
{
    public static void SeedDatabase(this WebApplication application)
    {
        var settings = application.Services.GetRequiredService<DatabaseConfigurationSettings>();

        if (settings.CreateDatabase)
        {
            CreateDatabaseAsync(application.Services).GetAwaiter().GetResult();
        }

        if (settings.SeedDatabase && settings.SeedData != null)
        {
            SeedDatabaseAsync(application.Services, settings.SeedData).GetAwaiter().GetResult();
        }
    }

    private async static Task CreateDatabaseAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.EnsureCreatedAsync();
        }
    }

    private async static Task SeedDatabaseAsync(IServiceProvider serviceProvider, IEnumerable<Employee> employees)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!await dbContext.Employees.AnyAsync())
            {
                await dbContext.Employees.AddRangeAsync(employees);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}