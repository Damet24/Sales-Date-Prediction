using Application.Shipper;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipperController : ControllerBase
{
    private readonly ShipperFinder _shipperFinder;

    public ShipperController(ShipperFinder shipperFinder)
    {
        _shipperFinder = shipperFinder;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_shipperFinder.GetAllShippers());
    }
}