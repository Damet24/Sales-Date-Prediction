using Application.Orders;
using Domain;
using Domain.Shipper.Repositories;
using Microsoft.Extensions.Logging;
using ShipperEntity = Domain.Shipper.Shipper;

namespace Application.Shipper;

public class ShipperFinder
{
    private readonly ILogger<OrderCreator> _logger;
    private readonly IShipperRepository _shipperRepository;

    public ShipperFinder(IShipperRepository shipperRepository, ILogger<OrderCreator> logger)
    {
        _shipperRepository = shipperRepository;
        _logger = logger;
    }

    public Result<List<ShipperEntity>> GetAllShippers() =>
        Result<List<ShipperEntity>>.Success(_shipperRepository.GetAllShippers());
}