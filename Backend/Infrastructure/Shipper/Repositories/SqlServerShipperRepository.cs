using Domain.Shipper.Repositories;
using Infrastructure.Clients;
using ShipperEntity = Domain.Shipper.Shipper;

namespace Infrastructure.Shipper.Repositories;

public class SqlServerShipperRepository : IShipperRepository
{
    private readonly SqlServerClient _client;

    public SqlServerShipperRepository(SqlServerClient client)
    {
        _client = client;
    }

    public List<ShipperEntity> GetAllShippers()
    {
        return _client.ExecuteQuery<ShipperEntity>(@"
                SELECT shipperid as Id, companyname as CompanyName, phone as Phone
                FROM StoreSample.Sales.Shippers;"
            );
    }
}