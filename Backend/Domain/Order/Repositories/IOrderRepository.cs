namespace Domain.Order.Repositories;

public interface IOrderRepository
{
    public List<Order> FindOrderByCustomerId(int customerId);
    public Result<string> Create(OrderWithDetails order);
}