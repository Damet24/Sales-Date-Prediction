using Domain;
using Domain.Constants;
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
        /*
         * Se valida que el usuario solo agregue un producto por orden.
         * Sin embargo, la lógica para agregar múltiples productos ya está implementada
         * en caso de que se necesite en el futuro.
         */
        return order.OrderDetails.Count > OrderConstants.MaxProductPerOrder
            ? Result<string>.Failure("Only one product can be added per order")
            : _orderRepository.Create(order);
    }
}