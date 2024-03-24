using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // Check if user already exists
        // Create User
        // Generate Token
        Guid userId = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var token = jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, "token");
    }
}
