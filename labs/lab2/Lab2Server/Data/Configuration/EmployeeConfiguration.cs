using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lab2Server.Data.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(b => b.EmployeeId);
        builder.Property(b => b.FirstName).HasMaxLength(255).IsRequired();
        builder.Property(b => b.LastName).HasMaxLength(255).IsRequired();
        builder.Property(b => b.DateOfEmployment).IsRequired();
    }
}