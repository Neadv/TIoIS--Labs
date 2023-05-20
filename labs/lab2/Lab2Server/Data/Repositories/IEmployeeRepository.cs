namespace Lab2Server.Data.Repositories;

public interface IEmployeeRepository 
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeByIdAsync(int id);
}