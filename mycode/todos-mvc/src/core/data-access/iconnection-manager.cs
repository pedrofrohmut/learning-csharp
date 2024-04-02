using System.Data;

namespace TodosMvc.Core.DataAccess;

public interface IConnectionManager
{
    IDbConnection GetConnection(String connectionString);
    void OpenConnection(IDbConnection? connection);
    void CloseConnection(IDbConnection? connection);
}
