using System.Data;
using Microsoft.AspNetCore.Mvc;
using TodosMvc.Core.Adapters.Web;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.UseCases.Goals;
using TodosMvc.DataAccess;
using TodosMvc.Mvc.Utils;

namespace TodosMvc.Mvc.Controllers;

/*
  1. Other controllers are not supposed to call the view directly.
  2. Other controllers should only call RedirectToAction and let PagesController
  call the views
*/
public class PagesController : Controller
{
    private readonly IConfiguration configuration;

    public PagesController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpGet("/")]
    public async Task<IActionResult> HomePage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;

        var authUserId = HttpContext.Session.GetString("authUserId");
        if (string.IsNullOrWhiteSpace(authUserId)) {
            return View("~/pages/index.cshtml");
        }

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            // Get connection
            var connectionString = ControllerUtils.GetConnectionString(this.configuration);
            connection = connectionManager.GetConnection(connectionString);

            // Instance useCase and its deps
            var userDataAccess = new UserDataAccess(connection);
            var goalDataAccess = new GoalDataAccess(connection);
            var listGoalsUseCase = new ListGoalsUseCase(userDataAccess, goalDataAccess);

            var response = await GoalsWebAdapter.ListGoals(listGoalsUseCase, authUserId);

            if (response.statusCode != 200 || response.body == null) {
                ViewData["errorMessage"] = response.message;
                return View("~/pages/index.cshtml");
            }

            List<GoalDbDto> goals = response.body!;

            return View("~/pages/index.cshtml", new { goals });
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpGet("/about")]
    public IActionResult AboutPage()
    {
        return View("~/pages/about.cshtml");
    }

    [HttpGet("/signup")]
    public IActionResult SignUpPage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;
        return View("~/pages/signup.cshtml");
    }

    [HttpGet("/signin")]
    public IActionResult SignInPage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;
        return View("~/pages/signin.cshtml");
    }
}
