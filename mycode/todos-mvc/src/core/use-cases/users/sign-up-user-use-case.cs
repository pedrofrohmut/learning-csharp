using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.Entities;
using TodosMvc.Core.Exceptions;
using TodosMvc.Core.Services;

namespace TodosMvc.Core.UseCases.Users;

public class SignUpUserUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IPasswordService passwordService;

    public SignUpUserUseCase(IUserDataAccess userDataAccess, IPasswordService passwordService)
    {
        this.userDataAccess = userDataAccess;
        this.passwordService = passwordService;
    }

    public async Task Execute(CreateUserDto newUser)
    {
        UserEntity.ValidateUser(newUser);
        Console.WriteLine("[Info] New User is valid");

        await UserEntity.CheckEmailAvailable(newUser.email!, this.userDataAccess);
        Console.WriteLine("[Info] E-mail is available");

        var passwordHash = await UserEntity.HashPassword(newUser.password!, this.passwordService);
        Console.WriteLine("[Info] Password hashed");

        await UserEntity.CreateUser(newUser, passwordHash, this.userDataAccess);
        Console.WriteLine("[Info] User created");
    }
}
