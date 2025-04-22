using System.Net;
using Application.Customer;
using Backend.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
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