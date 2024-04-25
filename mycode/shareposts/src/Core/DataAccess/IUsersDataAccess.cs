using System.Threading.Tasks;
using Shareposts.Core.Dtos.Db;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.Core.DataAccess;

public interface IUsersDataAccess
{
    Task<UserDbDto?> FindUserByEmail(string email);
    Task CreateUser(CreateUserDto newUser, string passwordHash);
}
