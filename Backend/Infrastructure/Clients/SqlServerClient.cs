using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Clients;

public class SqlServerClient
{
    private readonly string _connectionString;

    public SqlServerClient(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public List<T> ExecuteQuery<T>(string query, object? param = null)
    {
        using var connection = CreateConnection();
        connection.Open();
        return connection.Query<T>(query, param).ToList();
    }

    public T? ExecuteSingleQuery<T>(string query, object? param = null)
    {
        using var connection = CreateConnection();
        connection.Open();
        return connection.Query<T>(query, param).FirstOrDefault();
    }

    public void ExecuteNonQuery(string query, object? param = null)
    {
        using var connection = CreateConnection();
        connection.Open();
        connection.Execute(query, param);
    }
}