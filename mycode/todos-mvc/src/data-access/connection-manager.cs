using System.Data;
using TodosMvc.Core.DataAccess;

namespace TodosMvc.DataAccess;

public class ConnectionManager : IConnectionManager
{
    public void CloseConnection(IDbConnection? connection)
    {
        throw new NotImplementedException();
    }

    public IDbConnection GetConnection(string connectionString)
    {
        throw new NotImplementedException();
    }

    public void OpenConnection(IDbConnection? connection)
    {
        throw new NotImplementedException();
    }
}
