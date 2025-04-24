using Application.Customer;
using Application.Employee;
using Application.Orders;
using Backend.Extensions;
using Backend.Filters;
using Domain;
using Domain.Order;
using Infrastructure.Customers.Response;
using Infrastructure.Order.Request;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class OrdersController : ControllerBase
{
    private readonly OrderCreator _orderCreator;
    private readonly OrderOfCustomerFinder _orderOfCustomerFinder;

    public OrdersController(OrderFinder orderFinder, OrderCreator orderCreator, CustomerFinder customerFinder, OrderOfCustomerFinder orderOfCustomerFinder)
    {
        _orderCreator = orderCreator;
        _orderOfCustomerFinder = orderOfCustomerFinder;
    }

    [HttpGet("{customerId}")]
    public IActionResult Get(int customerId)
    {
        var result = _orderOfCustomerFinder.Find(customerId);
        if (!result.IsSuccess) return result.ToActionResult();
        var (customerName, orders) = result.Value;
        return Ok(new CustomerOrdersResponse
        {
            CustomerName = customerName,
            Orders = orders
        });
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrderRequest request) =>
        _orderCreator.Create((OrderWithDetails)request).ToActionResult();
}