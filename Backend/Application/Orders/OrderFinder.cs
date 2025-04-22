using Domain;
using Domain.Order;
using Domain.Order.Repositories;

namespace Application.Orders;

public class OrderFinder
{
    private readonly IOrderRepository _repository;
    
    public OrderFinder(IOrderRepository repository)
    {
        _repository = repository;
    }

    public Result<List<Order>> FindOrderByClient(int customerId)
    {
        var r = _repository.FindOrderByClientId(customerId);
        return r.Count > 0 ? Result<List<Order>>.Success(r) : Result<List<Order>>.Failure("Client not found");
    }
}