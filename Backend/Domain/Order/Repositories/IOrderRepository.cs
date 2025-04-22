namespace Domain.Order.Repositories;

public interface IOrderRepository
{
    public Order FindOrderByClientId(int clientId);
}