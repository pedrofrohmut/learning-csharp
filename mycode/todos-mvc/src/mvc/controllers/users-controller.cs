using System.Data;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
using TodosMvc.DataAccess;
using TodosMvc.Services;
using TodosMvc.Mvc.Utils;
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

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            // getConnection
            var connectionString = ControllerUtils.GetConnectionString(this.configuration);
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
                TempData["errorMessage"] = adaptedResponse.message;
                return RedirectToAction("SignUpPage", "Pages");
            }

            TempData["successMessage"] = adaptedResponse.message;
            return RedirectToAction("SignInPage", "Pages");
        } catch (Exception e) {
            return ControllerUtils.GetUnexpectedErrorResponse(e);
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInUser()
    {
        var userCredentials = new Core.Dtos.UserCredentialsDto() {
            email = Request.Form["email"],
            password = Request.Form["password"]
        };

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            // Get Connection
            var connectionString = ControllerUtils.GetConnectionString(this.configuration);
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            // Factory usecase
            var userDataAccess = new UserDataAccess(connection);
            var passwordService = new PasswordService();
            var signInUserUseCase = new SignInUserUseCase(userDataAccess, passwordService);

            // Make adaptedRequest
            var adaptedRequest = new WebAdaptedRequest() { body = userCredentials };

            // Call webadapter with useCase and adaptedRequest
            var adaptedResponse = await UsersWebAdapter.SignInUser(signInUserUseCase, adaptedRequest);

            string userId = adaptedResponse.body?.userId ?? "";

            if (adaptedResponse.statusCode != 200 || string.IsNullOrWhiteSpace(userId)) {
                TempData["errorMessage"] = adaptedResponse.message;
                return RedirectToAction("SignInPage", "Pages");
            }

	    // Add userId to session
            HttpContext.Session.SetString("authUserId", userId);

            return RedirectToAction("HomePage", "Pages");
        } catch (Exception e) {
            return ControllerUtils.GetUnexpectedErrorResponse(e);
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpGet("signout")]
    public IActionResult SignOutUser()
    {
        HttpContext.Session.SetString("authUserId", "");
        TempData["successMessage"] = "User signout successfully";
        return RedirectToAction("SignInPage", "Pages");
    }
}
