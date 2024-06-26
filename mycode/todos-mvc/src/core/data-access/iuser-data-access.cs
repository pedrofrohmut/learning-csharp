using TodosMvc.Core.Dtos;

namespace TodosMvc.Core.DataAccess;

public interface IUserDataAccess
{
    Task<UserDbDto?> FindUserByEmail(string email);
    Task<UserDbDto?> FindUserById(Guid userId);
    Task CreateUser(CreateUserDto newUser, string passwordHash);
}
