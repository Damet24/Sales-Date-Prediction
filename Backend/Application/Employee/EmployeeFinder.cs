using Application.Orders;
using Domain;
using Domain.Employee;
using Domain.Employee.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Employee;

public class EmployeeFinder
{
    private readonly ILogger<OrderCreator> _logger;
    private readonly IEmployeeRepository _repository;

    public EmployeeFinder(IEmployeeRepository repository, ILogger<OrderCreator> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Result<List<Domain.Employee.Employee>> GetEmployeeWhitOrderDates() =>
        Result<List<Domain.Employee.Employee>>.Success(_repository.GetAllEmployees());
}