using Microsoft.Data.SqlClient;
using Dapper;

namespace Infrastructure.Clients;

public class SqlServerClient
{
    public readonly SqlConnection Connection;
    public SqlTransaction? Transaction;

    public SqlServerClient(SqlConnection connection)
    {
        Connection = connection;
    }

    public void BeginTransaction()
    {
        if (Connection.State != System.Data.ConnectionState.Open)
            Connection.Open();

        Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction?.Commit();
        Transaction = null;
        Connection.Close();
    }

    public void Rollback()
    {
        Transaction?.Rollback();
        Transaction = null;
        Connection.Close();
    }

    public List<T> ExecuteQuery<T>(string query, object? param = null)
    {
        var result = Connection.Query<T>(query, param, Transaction);
        return result.ToList();
    }

    public T? ExecuteSingleQuery<T>(string query, object? param = null)
    {
        var result = Connection.Query<T>(query, param, Transaction);
        return result.FirstOrDefault();
    }

    public void ExecuteNonQuery(string query, object? param = null)
    {
        Connection.Execute(query, param, Transaction);
    }
}
