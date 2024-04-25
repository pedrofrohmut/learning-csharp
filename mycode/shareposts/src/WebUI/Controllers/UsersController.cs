using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Utils;
using Shareposts.DataAccess;
using Shareposts.Services;
using Shareposts.Core.UseCases.Users;
using Shareposts.Core.Adapters.Web;

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
}
