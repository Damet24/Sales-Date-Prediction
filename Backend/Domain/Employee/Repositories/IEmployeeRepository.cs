namespace Domain.Employee.Repositories;

public interface IEmployeeRepository
{
    public Domain.Employee.Employee GetEmployee(int id);
    
    public List<Domain.Employee.Employee> GetAllEmployees();
}