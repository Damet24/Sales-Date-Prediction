using Domain;
using Domain.Order;
using Domain.Order.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Orders;

public class OrderFinder
{
    private readonly ILogger<OrderCreator> _logger;
    private readonly IOrderRepository _repository;
    
    
    public OrderFinder(IOrderRepository repository, ILogger<OrderCreator> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Result<List<Order>> FindOrderByClient(int customerId)
    {
        var r = _repository.FindOrderByClientId(customerId);
        return r.Count > 0 ? Result<List<Order>>.Success(r) : Result<List<Order>>.Failure("Client not found");
    }
}