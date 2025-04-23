using Application.Employee;
using Application.Orders;
using Backend.Extensions;
using Backend.Filters;
using Domain;
using Domain.Order;
using Infrastructure.Order.Request;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class OrdersController : ControllerBase
{
    private readonly OrderFinder _orderFinder;
    private readonly OrderCreator _orderCreator;

    public OrdersController(OrderFinder orderFinder, OrderCreator orderCreator)
    {
        _orderFinder = orderFinder;
        _orderCreator = orderCreator;
    }

    [HttpGet("{customerId}")]
    public IActionResult Get(int customerId) => _orderFinder.FindOrderByClient(customerId).ToActionResult();

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrderRequest request) =>
        _orderCreator.Create((OrderWithDetails)request).ToActionResult();
}