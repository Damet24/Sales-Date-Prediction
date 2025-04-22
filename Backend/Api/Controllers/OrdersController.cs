using Application.Employee;
using Application.Orders;
using Backend.Extensions;
using Backend.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class OrdersController : ControllerBase
{
    private readonly OrderFinder _orderFinder;

    public OrdersController(OrderFinder orderFinder)
    {
        _orderFinder = orderFinder;
    }

    [HttpGet("{customerId}")]
    public IActionResult Get(int customerId) => _orderFinder.FindOrderByClient(customerId).ToActionResult();
}