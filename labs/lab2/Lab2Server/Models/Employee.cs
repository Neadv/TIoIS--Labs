namespace Lab2Server.Models;

public class Employee 
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfEmployment { get; set; }
}
