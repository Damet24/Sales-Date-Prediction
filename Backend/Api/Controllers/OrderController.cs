using Application.Employee;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OderFinder _orderFinder;
    
    public OrderController(OderFinder orderFinder)
    {
        _orderFinder = orderFinder;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_orderFinder.FindOrderByClient(0));
    }
}