using System.Data;
using TodosMvc.Core.DataAccess;
using Npgsql;

namespace TodosMvc.DataAccess;

public class ConnectionManager : IConnectionManager
{
    public IDbConnection GetConnection(string connectionString)
    {
        return new NpgsqlConnection(connectionString);
    }

    public void OpenConnection(IDbConnection? connection)
    {
        if (connection == null) {
            throw new ArgumentNullException("Connection is null. Cannot open it");
        }
        try {
            connection.Open();
            Console.WriteLine("!!! Connected to database !!!");
        } catch (Exception e) {
            throw new Exception("[ERROR] failed to open database connection: " + e.Message);
        }
    }

    public void CloseConnection(IDbConnection? connection)
    {
        if (connection == null) {
            throw new ArgumentNullException("Connection is null. Cannot close it");
        }
        try {
            connection.Close();
        } catch (Exception e) {
            throw new Exception("[ERROR] failed to close database connection: " + e.Message);
        }
    }
}
