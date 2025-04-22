using Application.Employee;
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
    public IActionResult Get()
    {
        return Ok(_employeeFinder.GetEmployeeWhitOrderDates());
    }
}