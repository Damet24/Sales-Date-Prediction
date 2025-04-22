using Application.Shippers.Repositories;
using Infrastructure.Clients;

namespace Infrastructure.Shipper.Repositories;

public class SqlServerShipperRepository : IShipperRepository
{
    private readonly SqlServerClient _client;

    public SqlServerShipperRepository(SqlServerClient client)
    {
        _client = client;
    }

    public List<Application.Shippers.Shipper> GetAllShippers()
    {
        return _client.ExecuteQuery<Application.Shippers.Shipper>("SELECT * FROM Shippers");
    }
}