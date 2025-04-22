using Domain.Order;
using Domain.Order.Repositories;
using Infrastructure.Clients;

namespace Infrastructure.Repositories;

public class SqlServerOrderRepository : IOrderRepository
{
    private readonly SqlServerClient _client;

    public SqlServerOrderRepository(SqlServerClient client)
    {
        _client = client;
    }

    public List<Order> FindOrderByClientId(int clientId)
    {
        return _client.ExecuteQuery<Order>("select * from StoreSample.Sales.Orders where custid = @custId",
            new { custId = clientId });
    }
}