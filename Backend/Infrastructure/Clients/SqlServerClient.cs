using Microsoft.Data.SqlClient;
using Dapper;

namespace Infrastructure.Clients;

public class SqlServerClient
{
    private readonly SqlConnection _connection;

    public SqlServerClient(SqlConnection connection)
    {
        _connection = connection;
    }

    public List<T> ExecuteQuery<T>(string query, object? param = null)
    {
        var result = _connection.Query<T>(query, param);
        return result.ToList();
    }
    
    public T? ExecuteSingleQuery<T>(string query, object? param = null)
    {
        var result = _connection.Query<T>(query, param);
        return result.ToList().FirstOrDefault();
    }

    public void ExecuteNonQuery(string query, object? param = null)
    {
        _connection.Execute(query, param);
    }
}