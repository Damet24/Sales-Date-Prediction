using EmployeeEntity  = Domain.Employee.Employee;
using Domain.Employee.Repositories;

namespace Application.Employee;

public class EmployeeFinder
{
    private readonly IEmployeeRepository _repository;

    public EmployeeFinder(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public List<EmployeeEntity> GetEmployeeWhitOrderDates()
    {
        return _repository.GetAllEmployees();
    }
}