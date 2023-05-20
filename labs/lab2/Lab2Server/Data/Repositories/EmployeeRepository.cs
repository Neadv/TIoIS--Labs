using Microsoft.EntityFrameworkCore;

namespace Lab2Server.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateEmployeeAsync(Employee employee)
    {
        await _dbContext.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEmployeeByIdAsync(int id)
    {
        var existedEmployee = await _dbContext.Employees.FindAsync(id);
        if (existedEmployee == null)
            throw new NullReferenceException(nameof(existedEmployee));

        _dbContext.Employees.Remove(existedEmployee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _dbContext.Update(employee);
        await _dbContext.SaveChangesAsync();
    }
}