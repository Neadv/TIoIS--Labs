using Lab2Client.Models;

namespace Lab2Client.Services
{
    public interface IEmployeeApiService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> DeleteEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
}