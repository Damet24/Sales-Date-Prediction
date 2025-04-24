namespace Infrastructure.Customers.Response;

public class CustomerOrdersResponse()
{
    public string CustomerName { get; set; }
    public List<Domain.Order.Order> Orders { get; set; } 
}