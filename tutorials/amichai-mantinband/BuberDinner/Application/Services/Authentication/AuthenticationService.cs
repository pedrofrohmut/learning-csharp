using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        if (this.userRepository.FindUserByEmail(email) != null) {
            throw new Exception("User e-mail already registered");
        }
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, FirstName = firstName, LastName = lastName, Email = email, Password = password };
        this.userRepository.AddUser(user);
        var token = this.jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        return new AuthenticationResult(userId, firstName, lastName, email, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        var user = this.userRepository.FindUserByEmail(email);
        if (user == null) {
            throw new Exception("No user found by this e-mail");
        }
        if (! user.Password.Equals(password)) {
            throw new Exception("Invalid Password");
        }
        var token = this.jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
        return new AuthenticationResult(user.Id, user.FirstName, user.LastName, email, token);
    }
}
