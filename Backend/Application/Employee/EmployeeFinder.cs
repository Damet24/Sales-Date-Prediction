using Domain;
using EmployeeEntity = Domain.Employee.Employee;
using Domain.Employee.Repositories;

namespace Application.Employee;

public class EmployeeFinder
{
    private readonly IEmployeeRepository _repository;

    public EmployeeFinder(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public Result<List<EmployeeEntity>> GetEmployeeWhitOrderDates() =>
        Result<List<EmployeeEntity>>.Success(_repository.GetAllEmployees());
}