using Domain;
using Domain.Customer.Repositories;
using Domain.Order;
using Domain.Order.Repositories;

namespace Application.Orders;

public class OrderOfCustomerFinder
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderOfCustomerFinder(IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public Result<(string, List<Order>)> Find(int customerId)
    {
        var customerInfo = _customerRepository.FindById(customerId);
        var orders = _orderRepository.FindOrderByCustomerId(customerId);
        return customerInfo == null
            ? Result<(string, List<Order>)>.Failure("Not Found")
            : Result<(string, List<Order>)>.Success((customerInfo.Name, orders));
    }
}