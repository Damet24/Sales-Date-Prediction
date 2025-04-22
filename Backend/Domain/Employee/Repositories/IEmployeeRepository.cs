namespace Domain.Employee.Repositories;

public interface IEmployeeRepository
{
    public Employee GetEmployee(int id);
    
    public List<Employee> GetAllEmployees();
}