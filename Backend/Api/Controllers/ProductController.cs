using Application.Product;
using Backend.Extensions;
using Backend.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(ExceptionFilter))]
public class ProductController
{
    private readonly ProductFinder _productFinder;

    public ProductController(ProductFinder productFinder)
    {
        _productFinder = productFinder;
    }

    [HttpGet]
    public IActionResult Get() => _productFinder.GetProducts().ToActionResult();
}