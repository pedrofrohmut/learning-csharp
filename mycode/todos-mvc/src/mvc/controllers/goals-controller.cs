using System.Data;
using Microsoft.AspNetCore.Mvc;
using TodosMvc.Core.Adapters.Web;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.UseCases.Goals;
using TodosMvc.DataAccess;
using TodosMvc.Mvc.Utils;

namespace TodosMvc.Mvc.Controllers;

[Route("goals")]
public class GoalsController : Controller
{
    private readonly IConfiguration configuration;

    public GoalsController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddGoal()
    {
        var authUserId = HttpContext.Session.GetString("authUserId");
        if (string.IsNullOrWhiteSpace(authUserId)) {
            return ControllerUtils.GetUnauthenticatedErrorResponse();
        }

        var newGoal = new CreateGoalDto() {
            content = Request.Form["content"],
            userId = authUserId,
        };

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            // Get connection
            var connectionString = ControllerUtils.GetConnectionString(this.configuration);
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            // Instance useCase and its deps
            var userDataAccess = new UserDataAccess(connection);
            var goalDataAccess = new GoalDataAccess(connection);
            var createGoalUseCase = new CreateGoalUseCase(userDataAccess, goalDataAccess);

            var response = await GoalsWebAdapter.CreateGoal(createGoalUseCase, newGoal, authUserId);

            if (response.statusCode != 201) {
                TempData["errorMessage"] = response.message;
            }

            return RedirectToAction("HomePage", "Pages");
        } catch (Exception e) {
            return ControllerUtils.GetUnexpectedErrorResponse(e);
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteGoal()
    {
        var authUserId = HttpContext.Session.GetString("authUserId");
        if (string.IsNullOrWhiteSpace(authUserId)) {
            return ControllerUtils.GetUnauthenticatedErrorResponse();
        }

        string? goalId = Request.Form["goalId"];

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;

        try {
            // Get connection
            var connectionString = ControllerUtils.GetConnectionString(this.configuration);
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            // Instance useCase and its deps
            var userDataAccess = new UserDataAccess(connection);
            var goalDataAccess = new GoalDataAccess(connection);
            var deleteGoalUseCase = new DeleteGoalUseCase(userDataAccess, goalDataAccess);

            var response = await GoalsWebAdapter.DeleteGoal(deleteGoalUseCase, goalId, authUserId);

            if (response.statusCode != 204) {
                TempData["errorMessage"] = response.message;
            }

            return RedirectToAction("HomePage", "Pages");
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }
}
