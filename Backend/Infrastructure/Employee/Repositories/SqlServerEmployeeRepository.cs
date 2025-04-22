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
            select
            empid as Id,
            (titleofcourtesy + ' ' + firstname + ' ' + lastname) as FullName,
            phone as Phone
            from StoreSample.HR.Employees where Id = @id", new { id });
    }

    public List<EmployeeEntity> GetAllEmployees()
    {
        return _client.ExecuteQuery<EmployeeEntity>(@"
            select  
            e.empid as Id,
            (e.titleofcourtesy + ' ' + e.firstname + ' ' + e.lastname) as FullName,
            e.phone as Phone
            from StoreSample.HR.Employees e where empid = 111;"
        );
    }
}
