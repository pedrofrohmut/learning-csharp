using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? FindUserByEmail(string email);
    void AddUser(User user);
}
