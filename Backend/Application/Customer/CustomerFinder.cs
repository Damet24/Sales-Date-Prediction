using CustomerEntity = Domain.Customer.Customer; 
    using Domain.Customer.Repositories;

namespace Application.Customer;

public class CustomerFinder
{
    private ICustomerRepository _repository;

    public CustomerFinder(ICustomerRepository repository)
    {
        this._repository = repository;
    }

    public List<CustomerWithOrderDate> GetCustomersWithOrderDate()
    {
        return _repository.GetCustomersWithOrderDate();
    }
}