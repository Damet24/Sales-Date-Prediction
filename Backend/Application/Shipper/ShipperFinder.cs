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
    
    public List<ShipperEntity> GetAllShippers()
    {
        return _shipperRepository.GetAllShippers();
    }
}