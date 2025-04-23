using Application.Product;
using Backend.Extensions;
using Backend.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class ProductsController
{
    private readonly ProductFinder _productFinder;

    public ProductsController(ProductFinder productFinder)
    {
        _productFinder = productFinder;
    }

    [HttpGet]
    public IActionResult Get() => _productFinder.GetProducts().ToActionResult();
}