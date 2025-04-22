using Domain;
using CustomerEntity = Domain.Customer.Customer;
using Domain.Customer.Repositories;

namespace Application.Customer;

public class CustomerFinder
{
    private readonly ICustomerRepository _repository;

    public CustomerFinder(ICustomerRepository repository)
    {
        this._repository = repository;
    }

    public Result<List<CustomerWithOrderDate>> GetCustomersWithOrderDate() =>
        Result<List<CustomerWithOrderDate>>.Success(_repository.GetCustomersWithOrderDate());
}