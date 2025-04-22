using Application.Employee;
using Application.Orders;
using Backend.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderFinder _orderFinder;
    
    public OrdersController(OrderFinder orderFinder)
    {
        _orderFinder = orderFinder;
    }
    
    [HttpGet("{orderId}")]
    public IActionResult Get(int orderId) => _orderFinder.FindOrderByClient(orderId).ToActionResult();
    
}