using Lab2Server.Configuration.Database;

namespace Lab2Server.Configuration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseConfigurationSettings)).Get<DatabaseConfigurationSettings>();
        services.AddSingleton(databaseSettings);

        return services;
    }
}