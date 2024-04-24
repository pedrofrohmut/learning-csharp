using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.Exceptions;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Services;

namespace Shareposts.Core.Entities;

public static class UserEntity
{
    public static void ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) {
            throw new UserValidationException("Name not provided. Name is required and cannot be null or blank.");
        }
        if (name.Length < 3) {
            throw new UserValidationException("Name is too short. Name must be at least 3 characters long.");
        }
        if (name.Length > 120) {
            throw new UserValidationException("Name is too long. Name must less than 120 characters long.");
        }
    }

    public static void ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) {
            throw new UserValidationException("E-mail not provided. E-mail is required and cannot be null or blank.");
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

    public static async Task CheckEmailAvailable(string email, IUserDataAccess userDataAccess)
    {
        var user = await userDataAccess.FindUserByEmail(email);
        if (user != null) {
            throw new UserEmailAlreadyRegisteredException();
        }
    }

    public static async Task<string> HashPassword(string password, IPasswordService passwordService)
    {
        return await passwordService.HashPassword(password);
    }

    public static async Task CreateUser(CreateUserDto newUser, string passwordHash, IUserDataAccess userDataAccess)
    {
        await userDataAccess.CreateUser(newUser, passwordHash);
    }
}
