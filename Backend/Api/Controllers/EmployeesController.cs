using Application.Employee;
using Backend.Extensions;
using Backend.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
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