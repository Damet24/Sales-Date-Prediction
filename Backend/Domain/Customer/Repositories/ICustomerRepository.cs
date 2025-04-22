namespace Domain.Customer.Repositories;

public interface ICustomerRepository
{
    public List<CustomerWithOrderDate> GetCustomersWithOrderDate();
}