using Application.Orders;
using Domain;
using EmployeeEntity = Domain.Employee.Employee;
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

    public Result<List<EmployeeEntity>> GetEmployeeWhitOrderDates() =>
        Result<List<EmployeeEntity>>.Success(_repository.GetAllEmployees());
}