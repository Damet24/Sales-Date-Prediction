using Application.Orders;
using Domain;
using CustomerEntity = Domain.Customer.Customer;
using Domain.Customer.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Customer;

public class CustomerFinder
{
    private readonly ILogger<OrderCreator> _logger;
    private readonly ICustomerRepository _repository;

    public CustomerFinder(ICustomerRepository repository, ILogger<OrderCreator> logger)
    {
        this._repository = repository;
        _logger = logger;
    }

    public Result<List<CustomerWithOrderDate>> GetCustomersWithOrderDate() =>
        Result<List<CustomerWithOrderDate>>.Success(_repository.GetCustomersWithOrderDate());
}