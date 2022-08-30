using Shop.Models;

namespace Shop.Repositories;

public class UserRepository
{
    public static User? Get(string username, string password) {
        var users = new List<User>();    
        users.Add(new User { Id=1, Username="Batman", Password="1234", Role="manager" });
        users.Add(new User { Id=2, Username="Robin",  Password="1234", Role="employee" });
        return users
            .Where(user => user.Username.ToLower() == username.ToLower())
            .FirstOrDefault();
    }
}
