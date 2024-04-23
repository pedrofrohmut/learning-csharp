using System.Data;

namespace Shareposts.Core.DataAccess;

public interface IConnectionManager
{
    IDbConnection GetConnection(string connectionString);
    void OpenConnection(IDbConnection? connection);
    void CloseConnection(IDbConnection? connection);
}
