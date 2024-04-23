using System;
using System.Threading.Tasks;
using Shareposts.Core.Dtos;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.UseCases.Users;
using Shareposts.Core.Exceptions;

namespace Shareposts.Core.Adapters.Web;

public static class UsersWebAdapter
{
    public static async Task<AdaptedWebResponse> SignUpUser(SignUpUserUseCase useCase, CreateUserDto newUser)
    {
        try {
            await useCase.Execute(newUser);
            return new AdaptedWebResponse() { statusCode = 201 };
        } catch (Exception e) when (
            e is UserValidationException || e is UserEmailAlreadyRegisteredException) {
            return new AdaptedWebResponse() { statusCode = 400, message = e.Message };
        }
    }
}
