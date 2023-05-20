namespace Lab2Server.Configuration.Database;

public class DatabaseConfigurationSettings
{
    public bool CreateDatabase { get; set; }
    public bool SeedDatabase { get; set; }
    public IEnumerable<Employee>? SeedData { get; set; }
}