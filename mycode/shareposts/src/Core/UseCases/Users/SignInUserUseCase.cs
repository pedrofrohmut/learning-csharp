using System;
using System.Threading.Tasks;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Services;
using Shareposts.Core.Entities;

namespace Shareposts.Core.UseCases.Users;

public class SignInUserUseCase
{
    private readonly IUsersDataAccess usersDataAccess;
    private readonly IPasswordService passwordService;
    private readonly IJwtService jwtService;

    public SignInUserUseCase(
        IUsersDataAccess usersDataAccess, IPasswordService passwordService, IJwtService jwtService)
    {
        this.usersDataAccess = usersDataAccess;
        this.passwordService = passwordService;
        this.jwtService = jwtService;
    }

    public async Task<SignInDto> Execute(UserCredentialsDto credentials)
    {
        UserEntity.ValidateUser(credentials);
        Console.WriteLine("[Info] New User is valid");

        var user = await UserEntity.CheckUserExists(credentials.email!, this.usersDataAccess);
        Console.WriteLine("[Info] User with this e-mail exists");

        await UserEntity.CheckPasswordMatches(credentials.password!, user.passwordHash!, this.passwordService);
        Console.WriteLine("[Info] User password hash matches this password");

        MainEntity.ValidateId(user.id);
        Console.WriteLine("[Info] User id is a valid Guid");

        var token = await UserEntity.CreateJwt(user.id!, this.jwtService);
        Console.WriteLine("[Info] Json Web Token created for this user");

        return new SignInDto() {
            userId = user.id!,
            name = user.name!,
            email = user.email!,
            authenticationToken = token
        };
    }
}
