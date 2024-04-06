using System.Data;
using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;

namespace TodosMvc.DataAccess;

public class UserDataAccess : IUserDataAccess
{
    private readonly IDbConnection connection;

    public UserDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    public Task CreateUser(CreateUserDto newUser, string passwordHash)
    {
        throw new NotImplementedException();
    }

    public Task<UserDbDto?> FindUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}
