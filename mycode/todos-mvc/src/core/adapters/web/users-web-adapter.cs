using TodosMvc.Core.Dtos;
using TodosMvc.Core.Exceptions;
using TodosMvc.Core.UseCases.Users;

namespace TodosMvc.Core.Adapters.Web;

public static class UsersWebAdapter
{
    public static async Task<WebAdaptedResponse> SignUpUser(SignUpUserUseCase useCase, WebAdaptedRequest req)
    {
        if (req.body == null) {
            return new WebAdaptedResponse() { statusCode = 400, message = "Empty Request Body" };
        }
        try {
            await useCase.Execute((CreateUserDto) req.body);
            return new WebAdaptedResponse() { statusCode = 201, message = "User created" };
        } catch (UserValidationException e) {
            return new WebAdaptedResponse() { statusCode = 400, message = e.Message };
        }
    }

    // TODO: think about change the signature to
    // Task<WebAdaptedResponse> SignInUser(SignInUserUseCase useCase, UserCredentialsDto body)
    public static async Task<WebAdaptedResponse> SignInUser(SignInUserUseCase useCase, WebAdaptedRequest req)
    {
        if (req.body == null) {
            return new WebAdaptedResponse() { statusCode = 400, message = "Empty Request Body" };
        }
        try {
            var signInDto = await useCase.Execute((UserCredentialsDto) req.body);
            return new WebAdaptedResponse() { statusCode = 200, body = signInDto };
        } catch (UserValidationException e) {
            return new WebAdaptedResponse() { statusCode = 400, message = e.Message };
        }
    }
}
