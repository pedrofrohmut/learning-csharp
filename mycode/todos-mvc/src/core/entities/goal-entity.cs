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

    public async static Task<GoalDbDto> CheckGoalExistsAndGet(Guid goalId, IGoalDataAccess goalDataAccess)
    {
        var goal = await goalDataAccess.FindGoalById(goalId);
        if (goal == null) {
            throw new GoalValidationException("Goal not found with the id passed");
        }
        return goal.Value;
    }

    public static void CheckGoalOwnership(GoalDbDto goal, Guid authUserId)
    {
        if (! string.Equals(goal.userId, authUserId)) {
            throw new GoalValidationException("Goal does not belong to the authenticated user");
        }
    }

    public async static Task DeleteGoal(Guid goalId, IGoalDataAccess goalDataAccess)
    {
        await goalDataAccess.DeleteGoal(goalId);
    }
}
