using Google.Protobuf.WellKnownTypes;

using Grpc.Net.Client;

using Lab2Client.Models;

using Lab2Server;

namespace Lab2Client.Services;

public class EmployeeApiService : IEmployeeApiService, IDisposable
{
    private readonly GrpcChannel _channel;
    private readonly EmployeeService.EmployeeServiceClient _client;

    public EmployeeApiService(IConfiguration configuration)
    {
        _channel = GrpcChannel.ForAddress(configuration.GetSection("EmployeeServerAddres").Get<string>());
        _client = new EmployeeService.EmployeeServiceClient(_channel);
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
        var result = await _client.GetEmployeeAsync(new GetEmployeeRequest { Id = id });
        return GetEmployeeFromResult(result);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var result = await _client.ListEmployeesAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return result.Employees.Select(e => GetEmployeeFromResult(e));
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        var result = await _client.CreateEmployeeAsync(new CreateEmployeeRequest
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfEmployment = employee.DateOfEmployment.ToUniversalTime().ToTimestamp()
        });

        return GetEmployeeFromResult(result);
    }

    public async Task<Employee> DeleteEmployeeByIdAsync(int id)
    {
        var result = await _client.DeleteEmployeeAsync(new DeleteEmployeeRequest { Id = id });
        return GetEmployeeFromResult(result);
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        var result = await _client.UpdateEmployeeAsync(new UpdateEmployeeRequest
        {
            Id = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfEmployment = employee.DateOfEmployment.ToUniversalTime().ToTimestamp()
        });
        return GetEmployeeFromResult(result);
    }

    private Employee GetEmployeeFromResult(EmployeeReply result)
    {
        return new Employee
        {
            EmployeeId = result.EmployeeId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            DateOfEmployment = result.DateOfEmployment.ToDateTime()
        };
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}