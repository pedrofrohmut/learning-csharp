using System.Data;
using Shareposts.Core.DataAccess;

namespace Shareposts.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IDbConnection connection;

    public UserDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }
}
