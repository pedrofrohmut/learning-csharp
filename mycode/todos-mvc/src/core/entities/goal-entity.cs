using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.Exceptions;

namespace TodosMvc.Core.Entities;

public static class GoalEntity
{
    public static void ValidateContent(string? content)
    {
        if (content == null) {
            throw new GoalValidationException("Content is null. Content is required and cannot be null.");
        }
        if (content.Length < 3) {
            throw new GoalValidationException("Content is too short. Min of 3 character is allowed.");
        }
        if (content.Length > 255) {
            throw new GoalValidationException("Content is too long. Max of 255 character is allowed.");
        }
    }

    public static void ValidateUserId(string? userId)
    {
        UtilsEntity.ValidateId(userId);
    }

    public static void ValidateGoal(CreateGoalDto newGoal)
    {
        GoalEntity.ValidateContent(newGoal.content);
        GoalEntity.ValidateUserId(newGoal.userId);
    }

    public async static Task CreateGoal(CreateGoalDto newGoal, Guid userId, IGoalDataAccess goalDataAccess)
    {
        await goalDataAccess.CreateGoal(newGoal, userId);
    }

    public async static Task<List<GoalDbDto>> ListGoals(Guid userId, IGoalDataAccess goalDataAccess)
    {
        var goals = await goalDataAccess.FindAllGoalsByUserId(userId);
        return goals;
    }
}
