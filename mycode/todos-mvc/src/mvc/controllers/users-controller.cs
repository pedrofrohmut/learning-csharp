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

[Route("api/users")]
public class UsersController : Controller
{
    private readonly IConfiguration configuration;

    public UsersController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost("signup")]
    public IActionResult SignUpUser([FromForm] NewUserDto newUser)
    {
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
            var adaptedResponse = UsersWebAdapter.SignUpUser(signUpUserUseCase, adaptedRequest);

            if (adaptedResponse.statusCode != 201) {
                return new ObjectResult(adaptedResponse.message) { StatusCode = adaptedResponse.statusCode };
            }

            return new ObjectResult(adaptedResponse.body) { StatusCode = adaptedResponse.statusCode };
        } catch (Exception e) {
            return new ObjectResult(e.Message) { StatusCode = 500 };
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
