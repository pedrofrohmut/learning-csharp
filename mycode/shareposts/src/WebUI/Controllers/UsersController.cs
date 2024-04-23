using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Utils;
using Shareposts.DataAccess;

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

        Console.WriteLine("Name: " + newUser.name);
        Console.WriteLine("E-mail: " + newUser.email);
        Console.WriteLine("Phone: " + newUser.phone);
        Console.WriteLine("Password: " + newUser.password);

        IDbConnection? connection = null;
        var connectionManager = new ConnectionManager();
        try {
            var connectionString = EnvUtils.GetConnectionString();
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            Console.WriteLine("Connection Working Successfully");

            return RedirectToAction("SignInUserPage", "Pages");
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }
}
