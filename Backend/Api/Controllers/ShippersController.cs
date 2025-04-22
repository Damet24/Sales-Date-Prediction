using Application.Shipper;
using Backend.Extensions;
using Backend.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
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