using Application.Employee;
using Backend.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeFinder _employeeFinder;
    
    public EmployeesController(EmployeeFinder employeeFinder)
    {
        _employeeFinder = employeeFinder;
    }
    
    
    [HttpGet]
    public IActionResult Get() => _employeeFinder.GetEmployeeWhitOrderDates().ToActionResult();
    
}