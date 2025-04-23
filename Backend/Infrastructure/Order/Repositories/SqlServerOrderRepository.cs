using System.Data;
using Domain;
using Domain.Order;
using Domain.Order.Repositories;
using Infrastructure.Clients;
using Infrastructure.Order.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Extensibility;

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

    public List<Domain.Order.Order> FindOrderByClientId(int customerId)
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
        _client.BeginTransaction();
        try
        {
            var details = CreateOrder(order);
            CreateOrderDetails(details.ToList());
            _client.Commit();
            return Result<string>.Success("Created");
        }
        catch (SqlException sqlException)
        {
            _logger.LogError($"{sqlException.Message}\n{sqlException.StackTrace}");
            _client.Rollback();

            return sqlException.Number switch
            {
                DatabaseErrors.ViolationOfConstraint or DatabaseErrors.ConflictedWithTheConstraint
                    or DatabaseErrors.CannotInsertDuplicateKeyRow => Result<string>.Failure(
                        "Database error in data validation"),
                _ => throw new Exception(sqlException.Message, sqlException)
            };
        }
        catch (Exception exception)
        {
            _logger.LogError($"{exception.Message}\n{exception.StackTrace}");
            _client.Rollback();
            throw;
        }
    }

    private void CreateOrderDetails(List<OrderDetail> details)
    {
        var table = ConvertToDataTable(details);
        using var bulkCopy = new SqlBulkCopy(_client.Connection, SqlBulkCopyOptions.Default, _client.Transaction);
        bulkCopy.DestinationTableName = table.TableName;
        bulkCopy.ColumnMappings.Add("OrderId", "orderid");
        bulkCopy.ColumnMappings.Add("ProductId", "productid");
        bulkCopy.ColumnMappings.Add("UnitPrice", "unitprice");
        bulkCopy.ColumnMappings.Add("Quantity", "qty");
        bulkCopy.ColumnMappings.Add("Discount", "discount");
        bulkCopy.WriteToServer(table);
    }

    private IEnumerable<OrderDetail> CreateOrder(OrderWithDetails order)
    {
        var sql = @"
        INSERT INTO StoreSample.Sales.Orders
            (
                custid,empid,orderdate,requireddate,shippeddate,shipperid,freight,shipname,shipaddress,shipcity,
                shipregion,shippostalcode,shipcountry
            )
            VALUES
            (
                @CustId,
                @EmpId,
                @OrderDate,
                @RequiredDate,
                @ShippedDate,
                @ShipperId,
                @Freight,
                @ShipName,
                @ShipAddress,
                @ShipCity,
                @ShipRegion,
                @ShipPostalCode,
                @ShipCountry
            );
           SELECT CAST(SCOPE_IDENTITY() AS int);";

        var orderId = _client.ExecuteSingleQuery<int>(sql, new
        {
            CustId = order.CustomerId,
            EmpId = order.EmployeeId,
            order.OrderDate,
            order.RequiredDate,
            order.ShippedDate,
            order.ShipperId,
            order.Freight,
            ShipName = order.ShipperName,
            ShipAddress = order.ShipperAddress,
            ShipCity = order.ShipperCity,
            ShipRegion = order.ShipperRegion,
            ShipPostalCode = order.ShipperPostalCode,
            ShipCountry = order.ShipperCountry
        });

        return order.OrderDetails.Select(item => new OrderDetail
        {
            OrderId = orderId,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            Discount = item.Discount,
        });
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