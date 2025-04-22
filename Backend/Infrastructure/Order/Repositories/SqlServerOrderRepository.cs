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

    public List<Order> FindOrderByClientId(int customerId)
    {
        return _client.ExecuteQuery<Order>(@"
			select 
				o.orderid as OrderId,
				o.custid as CustomerId,
				o.orderdate as OrderDate,
				o.requireddate as RequiredDate,
				o.shipaddress + ' ' + o.shipcity as Direction,
				s.companyname as ShipperName
			from StoreSample.Sales.Orders o 
			inner join StoreSample.Sales.Shippers s on o.shipperid = s.shipperid 
			where custid = @customerId",
            new { customerId = customerId });
    }
}