using Application.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerRepository : ControllerBase
{
    private readonly CustomerFinder customerFinder;

    public CustomerRepository(CustomerFinder customerFinder)
    {
        this.customerFinder = customerFinder;
    }
    
    [HttpGet("order-date")]
    public IActionResult GetWithOrderDate()
    {
        return Ok(customerFinder.GetCustomersWithOrderDate());
    }
}