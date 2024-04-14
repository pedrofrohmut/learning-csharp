using System.Text.RegularExpressions;
using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Services;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.Exceptions;

namespace TodosMvc.Core.Entities;

public static class UserEntity
{
    public static void ValidateName(string? name)
    {
        if (name == null) {
            throw new UserValidationException("Name is null. Name is required and cannot be null.");
        }
        if (name.Length < 3) {
            throw new UserValidationException("Name is too short. Min of 3 characters is allowed.");
        }
        if (name.Length > 120) {
            throw new UserValidationException("Name is too long. Max of 120 characters is allowed.");
        }
    }

    public static void ValidateEmail(string? email)
    {
        if (email == null) {
            throw new UserValidationException("E-mail is null. E-mail is required and cannot be null.");
        }
        if (!email.Contains("@") || !email.Contains(".")) {
            throw new UserValidationException(
                "E-mail is not in a valid format. E-mail pattern must contain both @ and .");
        }
        if (email.Length < 6) {
            throw new UserValidationException("E-mail is too short. Min of 6 characters for e-mail.");
        }
    }

    public static void ValidatePhone(string? phone)
    {
        if (phone == null) {
            throw new UserValidationException("Phone is null. Phone is required and cannot be null.");
        }
        var isPatternMatch = new Regex(@"\d{3}-\d{3}-\d{4}").IsMatch(phone);
        if (! isPatternMatch) {
            throw new UserValidationException("Phone pattern does not match.");
        }
    }

    public static void ValidatePassword(string? password)
    {
        if (password == null) {
            throw new UserValidationException("Password is null. Password is required and cannot be null.");
        }
        if (password.Length < 3) {
            throw new UserValidationException("Password is too short. Min of 3 characters is allowed.");
        }
        if (password.Length > 32) {
            throw new UserValidationException("Password is too long. Max of 32 characters is allowed.");
        }
    }

    public static void ValidateUser(CreateUserDto newUser)
    {
        UserEntity.ValidateName(newUser.name);
        UserEntity.ValidateEmail(newUser.email);
        UserEntity.ValidatePhone(newUser.phone);
        UserEntity.ValidatePassword(newUser.password);
    }

    public static void ValidateUser(UserCredentialsDto userCredentials)
    {
        UserEntity.ValidateEmail(userCredentials.email);
        UserEntity.ValidatePassword(userCredentials.password);
    }

    public async static Task CheckEmailAvailable(string email, IUserDataAccess userDataAccess)
    {
        var user = await userDataAccess.FindUserByEmail(email);
        if (user != null) {
            throw new UserValidationException(
                "E-mail is not available. E-mail is already registered and must be unique");
        }
    }

    public async static Task<string> HashPassword(string password, IPasswordService passwordService)
    {
        return await passwordService.HashPassword(password);
    }

    public async static Task<UserDbDto> FindUserByEmail(string email, IUserDataAccess userDataAccess)
    {
        var user = await userDataAccess.FindUserByEmail(email);
        if (user == null) {
            throw new UserValidationException("User not found with the passed e-mail");
        }
        return user.Value;
    }

    public async static Task CreateUser(CreateUserDto newUser, string passwordHash, IUserDataAccess userDataAccess)
    {
        await userDataAccess.CreateUser(newUser, passwordHash);
    }

    public async static Task VerifyPassword(string password, string passwordHash, IPasswordService passwordService)
    {
        bool isMatch = await passwordService.VerifyPassword(password, passwordHash);
        if (! isMatch) {
            throw new UserValidationException("User password does not match the passed e-mail");
        }
    }

    public async static Task<UserDbDto> FindUserById(Guid userId, IUserDataAccess userDataAccess)
    {
        var user = await userDataAccess.FindUserById(userId);
        if (user == null) {
            throw new UserValidationException("User not found with the passed userId");
        }
        return user.Value;
    }

    public async static Task CheckUserExists(Guid userId, IUserDataAccess userDataAccess)
    {
        var user = await userDataAccess.FindUserById(userId);
        if (user == null) {
            throw new UserValidationException("User not found with the passed userId");
        }
    }
}
