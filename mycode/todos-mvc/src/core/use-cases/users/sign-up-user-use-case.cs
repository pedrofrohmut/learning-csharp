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

        // TODO: move the if statement to UserEntity (rename to CheckEmailAvailable with no return)
        bool isEmailAvailable = await UserEntity.IsEmailAvailable(newUser.email!, this.userDataAccess);
        if (! isEmailAvailable) {
            throw new UserValidationException(
                "E-mail is not available. E-mail is already registered and must be unique");
        }
        Console.WriteLine("[Info] E-mail is available");

        var passwordHash = await UserEntity.HashPassword(newUser.password!, this.passwordService);
        Console.WriteLine("[Info] Password hashed");

        // TODO: Move it to UserEntity Method
        await this.userDataAccess.CreateUser(newUser, passwordHash);
        Console.WriteLine("[Info] User created");
    }
}
