using Domain.Order;
using Domain.Order.Repositories;

namespace Application.Orders;

public class OderFinder
{
    private readonly IOrderRepository _repository;
    
    public OderFinder(IOrderRepository repository)
    {
        _repository = repository;
    }

    public Order FindOrderByClient(int clientId)
    {
        return _repository.FindOrderByClientId(clientId);
    }
}