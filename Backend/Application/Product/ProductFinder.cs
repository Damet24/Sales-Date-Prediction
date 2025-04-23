using Application.Orders;
using Domain;
using Domain.Product.Repositories;
using Microsoft.Extensions.Logging;
using ProductEntity = Domain.Product.Product;

namespace Application.Product;

public class ProductFinder
{
    private readonly ILogger<OrderCreator> _logger;
    private readonly IProductRepository _repository;

    public ProductFinder(IProductRepository repository, ILogger<OrderCreator> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public Result<List<ProductEntity>> GetProducts()
    {
        return Result<List<ProductEntity>>.Success(_repository.GetProducts());
    }
}