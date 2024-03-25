using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = new List<User>();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public User? FindUserByEmail(string email)
    {
        return users.SingleOrDefault(x => x.Email.Equals(email));
    }
}
