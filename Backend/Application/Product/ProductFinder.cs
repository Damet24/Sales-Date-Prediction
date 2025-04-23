using Domain;
using Domain.Product.Repositories;
using ProductEntity = Domain.Product.Product;

namespace Application.Product;

public class ProductFinder
{
    private readonly IProductRepository _repository;

    public ProductFinder(IProductRepository repository)
    {
        _repository = repository;
    }


    public Result<List<ProductEntity>> GetProducts()
    {
        return Result<List<ProductEntity>>.Success(_repository.GetProducts());
    }
}