using Lab2Client.Models;
using Lab2Client.Models.ViewModels;
using Lab2Client.Services;

using Lab2Server;

using Microsoft.AspNetCore.Mvc;

namespace Lab2Client.Controllers;

public class EmployeeController: Controller
{
    private readonly IEmployeeApiService _employeeApiService;

    public EmployeeController(IEmployeeApiService employeeApiService)
    {
        _employeeApiService = employeeApiService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeApiService.GetAllAsync();
        return View(employees);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeApiService.GetEmployeeByIdAsync(id);

        return View(new EditEmployeeViewModel
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfEmployment = employee.DateOfEmployment
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditEmployeeViewModel employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        await _employeeApiService.UpdateEmployeeAsync(new Employee
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfEmployment = employee.DateOfEmployment
        });
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeViewModel employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        await _employeeApiService.CreateEmployeeAsync(new Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfEmployment = employee.DateOfEmployment
        });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _employeeApiService.DeleteEmployeeByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }
}