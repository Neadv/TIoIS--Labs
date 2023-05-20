using Lab2Server.Data.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Lab2Server.Configuration;

public static class DataConfigurationExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        return services;
    }
}