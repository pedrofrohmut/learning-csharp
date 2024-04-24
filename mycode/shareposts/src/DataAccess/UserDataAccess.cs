using System.Data;
using System.Threading.Tasks;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.Db;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IDbConnection connection;

    public UserDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    public Task<UserDbDto?> FindUserByEmail(string email)
    {
        throw new System.NotImplementedException();
    }

    public Task CreateUser(CreateUserDto newUser, string passwordHash)
    {
        throw new System.NotImplementedException();
    }
}
