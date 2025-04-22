using Application.Shipper;
using Backend.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ShippersController : ControllerBase
{
    private readonly ShipperFinder _shipperFinder;

    public ShippersController(ShipperFinder shipperFinder)
    {
        _shipperFinder = shipperFinder;
    }

    [HttpGet]
    public IActionResult Get() => _shipperFinder.GetAllShippers().ToActionResult();
}