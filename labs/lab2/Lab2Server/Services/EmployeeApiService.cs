using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Lab2Server.Data.Repositories;

namespace Lab2Server.Services;

public class EmployeeApiService: EmployeeService.EmployeeServiceBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeApiService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override async Task<EmployeeReply> CreateEmployee(CreateEmployeeRequest request, ServerCallContext context)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfEmployment = request.DateOfEmployment.ToDateTime()
        };

        await _employeeRepository.CreateEmployeeAsync(employee);

        return GetEmployeeReplyFromEmployee(employee);
    }
    

    public override async Task<EmployeeReply> DeleteEmployee(DeleteEmployeeRequest request, ServerCallContext context)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if (employee == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));

        await _employeeRepository.DeleteEmployeeByIdAsync(employee.EmployeeId);

        return GetEmployeeReplyFromEmployee(employee);
    }

    public async override Task<EmployeeReply> GetEmployee(GetEmployeeRequest request, ServerCallContext context)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if (employee == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));

        return GetEmployeeReplyFromEmployee(employee);
    }

    public override async Task<ListReply> ListEmployees(Empty request, ServerCallContext context)
    {
        var employess = await _employeeRepository.GetAllAsync();
        var listReply = new ListReply();
        listReply.Employees.AddRange(employess.Select(e => GetEmployeeReplyFromEmployee(e)));
        return listReply;
    }

    public async override Task<EmployeeReply> UpdateEmployee(UpdateEmployeeRequest request, ServerCallContext context)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if (employee == null)
            throw new RpcException(new Status(StatusCode.NotFound, "Employee not found"));

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.DateOfEmployment = request.DateOfEmployment.ToDateTime();

        await _employeeRepository.UpdateEmployeeAsync(employee);

        return GetEmployeeReplyFromEmployee(employee);
    }

    private static EmployeeReply GetEmployeeReplyFromEmployee(Employee employee)
    {
        return new EmployeeReply
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfEmployment = employee.DateOfEmployment.ToUniversalTime().ToTimestamp()
        };
    }
}