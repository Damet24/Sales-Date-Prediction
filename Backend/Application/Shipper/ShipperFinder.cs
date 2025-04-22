using Domain;
using Domain.Shipper.Repositories;
using ShipperEntity = Domain.Shipper.Shipper;

namespace Application.Shipper;

public class ShipperFinder
{
    private readonly IShipperRepository _shipperRepository;

    public ShipperFinder(IShipperRepository shipperRepository)
    {
        _shipperRepository = shipperRepository;
    }

    public Result<List<ShipperEntity>> GetAllShippers() =>
        Result<List<ShipperEntity>>.Success(_shipperRepository.GetAllShippers());
}