using Domain;
using Domain.Order;
using Domain.Order.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Orders;

public class OrderCreator
{
    private readonly ILogger<OrderCreator> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderCreator(IOrderRepository orderRepository, ILogger<OrderCreator> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public Result<string> Create(OrderWithDetails order)
    {
        return _orderRepository.Create(order);
    }
}