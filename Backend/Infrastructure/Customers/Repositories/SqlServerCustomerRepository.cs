using Domain.Customer;
using Domain.Customer.Repositories;
using Infrastructure.Clients;

namespace Infrastructure.Customers.Repositories;

public class SqlServerCustomerRepository : ICustomerRepository
{
    private readonly SqlServerClient _client;

    public SqlServerCustomerRepository(SqlServerClient client)
    {
        _client = client;
    }

    public List<CustomerWithOrderDate> GetCustomersWithOrderDate()
    {
        return _client.ExecuteQuery<CustomerWithOrderDate>(@"
            WITH orders_with_interval AS (
                SELECT o.custid, 
                       o.orderdate, 
                       LAG(o.orderdate) OVER (PARTITION BY o.custid ORDER BY o.orderdate) AS previous_order_date 
                FROM StoreSample.Sales.Orders o
            ),
            intervals AS (
                SELECT custid, 
                       DATEDIFF(DAY, previous_order_date, orderdate) AS diff_day 
                FROM orders_with_interval 
                WHERE previous_order_date IS NOT NULL
            ),
            averages AS (
                SELECT custid, 
                       AVG(diff_day) AS average_day 
                FROM intervals 
                GROUP BY custid
            ),
            last_orders AS (
                SELECT custid, 
                       MAX(orderdate) AS last_date 
                FROM StoreSample.Sales.Orders 
                GROUP BY custid 
            )
            SELECT
                c.custid as Id,
                c.contactname as CustomerName,
                lo.last_date as LastOrderDate,
                DATEADD(DAY, p.average_day, lo.last_date) as NextPredictedOrder
            FROM StoreSample.Sales.Customers c
            JOIN last_orders lo ON c.custid = lo.custid
            JOIN averages p ON c.custid = p.custid
        ");
    }

    public Customer? FindById(int customerId)
    {
        return _client.ExecuteSingleQuery<Customer>(
            "select companyname as Name from StoreSample.Sales.Customers where custid = @customerId", new { customerId });
    }
}