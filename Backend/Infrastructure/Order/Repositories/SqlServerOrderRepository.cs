using System.Data;
using Dapper;
using Domain;
using Domain.Order;
using Domain.Order.Repositories;
using Infrastructure.Clients;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Order.Repositories;

public class SqlServerOrderRepository : IOrderRepository
{
    private readonly ILogger<SqlServerOrderRepository> _logger;
    private readonly SqlServerClient _client;

    public SqlServerOrderRepository(SqlServerClient client, ILogger<SqlServerOrderRepository> logger)
    {
        _client = client;
        _logger = logger;
    }

    public List<Domain.Order.Order> FindOrderByCustomerId(int customerId)
    {
        return _client.ExecuteQuery<Domain.Order.Order>(@"
			SELECT 
			    orderid AS Id, 
			    requireddate AS RequiredDate, 
			    shippeddate AS ShippedDate, 
			    shipname AS ShipperName, 
			    shipaddress AS ShipperAddress, 
			    shipcity AS ShipperCity   
			FROM StoreSample.Sales.Orders 
			WHERE custid = @customerId",
            new { customerId });
    }

    public Result<string> Create(OrderWithDetails order)
{
    using var connection = _client.CreateConnection();
    connection.Open();
    using var transaction = connection.BeginTransaction();

    try
    {
        var (details, orderId) = CreateOrder(order, connection, transaction);
        CreateOrderDetails(details.ToList(), connection, transaction);

        transaction.Commit();
        return Result<string>.Success(orderId.ToString());
    }
    catch (Exception exception)
    {
        _logger.LogError($"{exception.Message}\n{exception.StackTrace}");
        transaction.Rollback();
        return Result<string>.Failure(exception.Message);
    }
}

private void CreateOrderDetails(List<OrderDetail> details, SqlConnection connection, SqlTransaction transaction)
{
    var table = ConvertToDataTable(details);

    using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
    {
        DestinationTableName = "[StoreSample].[Sales].[OrderDetails]"
    };

    bulkCopy.ColumnMappings.Add("OrderId", "orderid");
    bulkCopy.ColumnMappings.Add("ProductId", "productid");
    bulkCopy.ColumnMappings.Add("UnitPrice", "unitprice");
    bulkCopy.ColumnMappings.Add("Quantity", "qty");
    bulkCopy.ColumnMappings.Add("Discount", "discount");

    bulkCopy.WriteToServer(table);
}

private (IEnumerable<OrderDetail>, int orderId) CreateOrder(OrderWithDetails order, SqlConnection connection, SqlTransaction transaction)
{
    const string sql = @"
        INSERT INTO StoreSample.Sales.Orders
            (
                custid, empid, orderdate, requireddate, shippeddate, shipperid, freight,
                shipname, shipaddress, shipcity, shipregion, shippostalcode, shipcountry
            )
        VALUES
            (
                @CustId, @EmpId, @OrderDate, @RequiredDate, @ShippedDate, @ShipperId, @Freight,
                @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry
            );
        SELECT CAST(SCOPE_IDENTITY() AS int);";

    var orderId = connection.QuerySingle<int>(sql, new
    {
        CustId = order.CustomerId,
        EmpId = order.EmployeeId,
        order.OrderDate,
        order.RequiredDate,
        order.ShippedDate,
        order.ShipperId,
        order.Freight,
        order.ShipName,
        order.ShipAddress,
        order.ShipCity,
        order.ShipRegion,
        order.ShipPostalCode,
        order.ShipCountry
    }, transaction);

    return (order.OrderDetails.Select(item => new OrderDetail
    {
        OrderId = orderId,
        ProductId = item.ProductId,
        Quantity = item.Quantity,
        UnitPrice = item.UnitPrice,
        Discount = item.Discount
    }), orderId);
}

    private DataTable ConvertToDataTable(List<OrderDetail> details)
    {
        var table = new DataTable();
        table.TableName = "StoreSample.Sales.OrderDetails";

        table.Columns.Add("OrderId", typeof(int));
        table.Columns.Add("ProductId", typeof(int));
        table.Columns.Add("UnitPrice", typeof(decimal));
        table.Columns.Add("Quantity", typeof(int));
        table.Columns.Add("Discount", typeof(decimal));

        foreach (var detail in details)
        {
            table.Rows.Add(detail.OrderId, detail.ProductId, detail.UnitPrice, detail.Quantity, detail.Discount);
        }

        return table;
    }
}