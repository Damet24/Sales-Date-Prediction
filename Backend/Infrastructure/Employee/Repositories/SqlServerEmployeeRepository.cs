using Domain.Employee;
using Domain.Employee.Repositories;
using Infrastructure.Clients;

namespace Infrastructure.Employee.Repositories;

public class SqlServerEmployeeRepository : IEmployeeRepository
{
    private readonly SqlServerClient _client;

    public SqlServerEmployeeRepository(SqlServerClient client)
    {
        _client = client;
    }
    
    public Domain.Employee.Employee GetEmployee(int id)
    {
        return _client.ExecuteSingleQuery<Domain.Employee.Employee>(@"
            SELECT
            empid AS Id,
            (titleofcourtesy + ' ' + firstname + ' ' + lastname) AS FullName
            FROM StoreSample.HR.Employees WHERE Id = @id", new { id });
    }

    public List<Domain.Employee.Employee> GetAllEmployees()
    {
        return _client.ExecuteQuery<Domain.Employee.Employee>(@"
            SELECT  
            e.empid AS Id,
            (e.titleofcourtesy + ' ' + e.firstname + ' ' + e.lastname) AS FullName
            FROM StoreSample.HR.Employees e;"
        );
    }
}
