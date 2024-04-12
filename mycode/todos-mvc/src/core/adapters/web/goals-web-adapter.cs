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
}
