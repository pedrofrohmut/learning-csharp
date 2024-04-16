using TodosMvc.Core.Dtos;
using TodosMvc.Core.Exceptions;
using TodosMvc.Core.UseCases.Goals;

namespace TodosMvc.Core.Adapters.Web;

public static class GoalsWebAdapter
{
    public async static Task<WebAdaptedResponse> CreateGoal(
        CreateGoalUseCase useCase, CreateGoalDto newGoal, string authUserId)
    {
        try {
            await useCase.Execute(newGoal, authUserId);
            return new WebAdaptedResponse() { statusCode = 201, message = "Goal Created" };
        } catch (GoalValidationException e) {
            return new WebAdaptedResponse() { statusCode = 400, message = e.Message };
        }
    }

    public async static Task<WebAdaptedResponse> ListGoals(
            ListGoalsUseCase useCase, string authUserId)
    {
        try {
            List<GoalDbDto> goals = await useCase.Execute(authUserId);
            return new WebAdaptedResponse() { statusCode = 200, body = goals };
        } catch (UserValidationException e) {
            return new WebAdaptedResponse() { statusCode = 404, message = e.Message };
        }
    }

    public static async Task<WebAdaptedResponse> DeleteGoal(
            DeleteGoalUseCase useCase, string? goalId, string authUserId)
    {
        try {
            await useCase.Execute(goalId, authUserId);
            return new WebAdaptedResponse() { statusCode = 204, message = ""};
        } catch (Exception e) when (e is UserValidationException || e is GoalValidationException) {
            return new WebAdaptedResponse() { statusCode = 400, message = e.Message };
        }
    }
}
