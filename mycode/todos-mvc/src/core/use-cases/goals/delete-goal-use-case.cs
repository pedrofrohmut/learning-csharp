using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Entities;

namespace TodosMvc.Core.UseCases.Goals;

public class DeleteGoalUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IGoalDataAccess goalDataAccess;

    public DeleteGoalUseCase(IUserDataAccess userDataAccess, IGoalDataAccess goalDataAccess)
    {
        this.userDataAccess = userDataAccess;
        this.goalDataAccess = goalDataAccess;
    }

    public async Task Execute(string? goalId, string? authUserId)
    {
        var validUserId = UtilsEntity.CastValidateId(authUserId);
        Console.WriteLine("[Info] Auth user id is valid");

        await UserEntity.CheckUserExists(validUserId, this.userDataAccess);
        Console.WriteLine("[Info] User with id equal to authUserId exists");

        var validGoalId = UtilsEntity.CastValidateId(goalId);
        Console.WriteLine("[Info] Goal id is valid");

        var goal = await GoalEntity.CheckGoalExistsAndGet(validGoalId, this.goalDataAccess);
        Console.WriteLine("[Info] Goal with id equal to validGoalId exists");

        GoalEntity.CheckGoalOwnership(goal, validUserId);
        Console.WriteLine("[Info] Goal found belong to the authenticated user");

        await GoalEntity.DeleteGoal(validGoalId, this.goalDataAccess);
        Console.WriteLine("[Info] Goal deleted");
    }
}
