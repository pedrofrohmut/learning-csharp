using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.Entities;
using TodosMvc.Core.Services;

namespace TodosMvc.Core.UseCases.Users;

public class SignInUserUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IPasswordService passwordService;

    public SignInUserUseCase(IUserDataAccess userDataAccess, IPasswordService passwordService)
    {
        this.userDataAccess = userDataAccess;
        this.passwordService = passwordService;
    }

    public async Task<SignInDto> Execute(UserCredentialsDto userCredentials)
    {
        UserEntity.ValidateUser(userCredentials);
        Console.WriteLine("[Info] User credentials are valid");

        var user = await UserEntity.FindUserByEmail(userCredentials.email!, this.userDataAccess);
        Console.WriteLine("[Info] User with the same e-mail found");

        await UserEntity.VerifyPassword(userCredentials.password!, user.passwordHash!, this.passwordService);
        Console.WriteLine("[Info] User password matches");

        UtilsEntity.ValidateId(user.id);
        Console.WriteLine("[Info] User id is valid");

        return new SignInDto() { userId = user.id?.ToString() ?? "" };
    }
}
