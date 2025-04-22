namespace Domain.Order.Repositories;

public interface IOrderRepository
{
    public List<Order> FindOrderByClientId(int customerId);
}