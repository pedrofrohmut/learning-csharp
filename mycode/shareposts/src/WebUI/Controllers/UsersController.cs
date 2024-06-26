using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Utils;
using Shareposts.DataAccess;
using Shareposts.Services;
using Shareposts.Core.UseCases.Users;
using Shareposts.Core.Adapters.Web;
using Shareposts.WebUI.Utils;

namespace Shareposts.WebUI.Controllers;

[Route("Users")]
public class UsersController : Controller
{
    private readonly IConfiguration configuration;

    public UsersController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUpUser()
    {
        var (isNotAuthenticated, action, controller) =
            ControllerUtils.IsNotAuthenticatedOrRedirect(Request, TempData);
        if (! isNotAuthenticated) {
            return RedirectToAction(action, controller);
        }

        var newUser = new CreateUserDto() {
            name = Request.Form["name"],
            email = Request.Form["email"],
            phone = Request.Form["phone"],
            password = Request.Form["password"]
        };

        IDbConnection? connection = null;
        var connectionManager = new ConnectionManager();
        try {
            var connectionString = EnvUtils.GetConnectionString();

            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            var usersDataAccess = new UsersDataAccess(connection);
            var passwordService = new PasswordService();
            var signUpUserUseCase = new SignUpUserUseCase(usersDataAccess, passwordService);

            var response = await UsersWebAdapter.SignUpUser(signUpUserUseCase, newUser);

            if (response.statusCode != 201) {
                TempData["errorMessage"] = response.message;
                return RedirectToAction("SignUpUserPage", "Pages");
            }

            TempData["successMessage"] = "User created successfully";
            return RedirectToAction("SignInUserPage", "Pages");
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignInUser()
    {
        var (isNotAuthenticated, action, controller) =
            ControllerUtils.IsNotAuthenticatedOrRedirect(Request, TempData);
        if (! isNotAuthenticated) {
            return RedirectToAction(action, controller);
        }

        var credentials = new UserCredentialsDto() {
            email = Request.Form["email"],
            password = Request.Form["password"]
        };

        IDbConnection? connection = null;
        var connectionManager = new ConnectionManager();
        try {
            var connectionString = EnvUtils.GetConnectionString();
            var jwtSecret = EnvUtils.GetJwtSecret();

            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            var usersDataAccess = new UsersDataAccess(connection);
            var passwordService = new PasswordService();
            var jwtService = new JwtService(jwtSecret);
            var signInUserUseCase = new SignInUserUseCase(usersDataAccess, passwordService, jwtService);

            var response = await UsersWebAdapter.SignInUser(signInUserUseCase, credentials);

            if (response.statusCode != 200) {
                TempData["errorMessage"] = response.message;
                return RedirectToAction("SignInUserPage", "Pages");
            }

            var cookieOptions = new CookieOptions() {
                Expires = DateTime.UtcNow.AddDays(7),
                IsEssential = true,
            };
            Response.Cookies.Append("authToken", response.body?.token);

            return RedirectToAction("HomePage", "Pages");
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpGet("SignOut")]
    public IActionResult SignOutUser()
    {
        var (isAuthenticated, action, controller) =
            ControllerUtils.IsAuthenticatedOrRedirect(Request, TempData);
        if (! isAuthenticated) {
            return RedirectToAction(action, controller);
        }

        Response.Cookies.Delete("authToken");

        return RedirectToAction("SignInUserPage", "Pages");
    }
}
