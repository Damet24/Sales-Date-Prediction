using Domain.Employee.Repositories;
using EmployeeEntity = Domain.Employee.Employee;
using Infrastructure.Clients;

namespace Infrastructure.Employee.Repositories;

public class SqlServerEmployeeRepository : IEmployeeRepository
{
    private readonly SqlServerClient _client;

    public SqlServerEmployeeRepository(SqlServerClient client)
    {
        _client = client;
    }
    
    public EmployeeEntity GetEmployee(int id)
    {
        return _client.ExecuteSingleQuery<EmployeeEntity>(@"
            SELECT
            empid AS Id,
            (titleofcourtesy + ' ' + firstname + ' ' + lastname) AS FullName
            FROM StoreSample.HR.Employees WHERE Id = @id", new { id });
    }

    public List<EmployeeEntity> GetAllEmployees()
    {
        return _client.ExecuteQuery<EmployeeEntity>(@"
            SELECT  
            e.empid AS Id,
            (e.titleofcourtesy + ' ' + e.firstname + ' ' + e.lastname) AS FullName
            FROM StoreSample.HR.Employees e;"
        );
    }
}
