using Domain.Product;
using Domain.Product.Repositories;
using Infrastructure.Clients;

namespace Infrastructure.Products;

public class SqlServerProductRepository : IProductRepository
{
    private readonly SqlServerClient _client;

    public SqlServerProductRepository(SqlServerClient client)
    {
        _client = client;
    }
    
    public List<Product> GetProducts()
    {
        return _client.ExecuteQuery<Product>(@"
            SELECT
            productid AS Id,
            productname AS ProductName
            FROM StoreSample.Production.Products
            WHERE discontinued != 1;
        ");
    }
}