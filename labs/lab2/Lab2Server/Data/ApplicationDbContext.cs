using Lab2Server.Data.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Lab2Server.Data;

public class ApplicationDbContext: DbContext
{
    public DbSet<Employee> Employees { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}