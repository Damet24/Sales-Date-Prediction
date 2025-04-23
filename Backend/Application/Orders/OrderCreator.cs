using Domain;
using Domain.Order;
using Domain.Order.Repositories;

namespace Application.Orders;

public class OrderCreator
{
    private readonly IOrderRepository _orderRepository;

    public OrderCreator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Result<string> Create(OrderWithDetails order)
    {
        return _orderRepository.Create(order);
    }
}