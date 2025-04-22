using System.Net;
using Application.Customer;
using Backend.Extensions;
using Backend.Filters;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class CustomersController : ControllerBase
{
    private readonly CustomerFinder _customerFinder;

    public CustomersController(CustomerFinder customerFinder)
    {
        this._customerFinder = customerFinder;
    }
    
    [HttpGet("order-activity")]
    public IActionResult GetWithOrderDate() => _customerFinder.GetCustomersWithOrderDate().ToActionResult();
    
}   