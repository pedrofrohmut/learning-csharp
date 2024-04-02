using System.Data;
using TodosMvc.Core.DataAccess;

namespace TodosMvc.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IDbConnection connection;

    public UserDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }
}
