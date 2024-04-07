using System.Data;
using Microsoft.AspNetCore.Mvc;
using TodosMvc.DataAccess;
using TodosMvc.Services;
using TodosMvc.Mvc.Utils;
using TodosMvc.Mvc.Dtos.Users;
using TodosMvc.Core.UseCases.Users;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.Adapters.Web;

namespace TodosMvc.Mvc.Controllers;

/*
  TODO: discover how to log in/out with dotnet mvc without JS
*/

[Route("users")]
public class UsersController : Controller
{
    private readonly IConfiguration configuration;

    public UsersController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUpUser()
    {
        var newUser = new CreateUserDto() {
            name = Request.Form["name"],
            email = Request.Form["email"],
            phone = Request.Form["phone"],
            password = Request.Form["password"],
        };

        ConnectionManager? connectionManager = null;
        IDbConnection? connection = null;
        try {
            // getConnection
            var connectionString = ControllerUtils.GetConnectionString(this.configuration);
            connectionManager = new ConnectionManager();
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            // instanceUseCase and deps
            var userDataAccess = new UserDataAccess(connection);
            var passwordService = new PasswordService();
            var signUpUserUseCase = new SignUpUserUseCase(userDataAccess, passwordService);

            // Make adaptedRequest
            var adaptedRequest = new WebAdaptedRequest() { body = newUser };

            // Call webAdapter with the instance of useCase and adaptedRequest
            var adaptedResponse = await UsersWebAdapter.SignUpUser(signUpUserUseCase, adaptedRequest);

            if (adaptedResponse.statusCode != 201) {
                TempData["ErrorMessage"] = adaptedResponse.message;
                return RedirectToAction("SignUpPage", "Pages");
            }

            TempData["SuccessMessage"] = adaptedResponse.message;
            return RedirectToAction("SignInPage", "Pages");
        } catch (Exception e) {
            return new ObjectResult("Unexpected Error: " + e.Message) { StatusCode = 500 };
        } finally {
            if (connectionManager != null) {
                connectionManager.CloseConnection(connection);
            }
        }
    }

    [HttpPost("signin")]
    public IActionResult SignInUser()
    {
        return new ObjectResult("Sign in user") { StatusCode = 200 };
    }

    [HttpPost("signout")]
    public IActionResult SignOutUser()
    {
        return new ObjectResult("") { StatusCode = 204 };
    }
}
