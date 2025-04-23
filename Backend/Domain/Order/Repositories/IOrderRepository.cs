namespace Domain.Order.Repositories;

public interface IOrderRepository
{
    public List<Order> FindOrderByClientId(int customerId);
    public Result<string> Create(OrderWithDetails order);
}