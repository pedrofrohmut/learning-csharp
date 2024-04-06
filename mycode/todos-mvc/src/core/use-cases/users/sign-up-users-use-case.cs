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
        bool isEmailAvailable = await UserEntity.IsEmailAvailable(newUser.email!, this.userDataAccess);
        if (! isEmailAvailable) {
            throw new UserValidationException(
                "E-mail is not available. E-mail is already registered and must be unique");
        }
        var passwordHash = await UserEntity.HashPassword(newUser.password!, this.passwordService);
        await this.userDataAccess.CreateUser(newUser, passwordHash);
    }
}
