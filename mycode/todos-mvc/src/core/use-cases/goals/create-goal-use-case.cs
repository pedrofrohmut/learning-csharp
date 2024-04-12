using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;
using TodosMvc.Core.Entities;

namespace TodosMvc.Core.UseCases.Goals;

public class CreateGoalUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IGoalDataAccess goalDataAccess;

    public CreateGoalUseCase(IUserDataAccess userDataAccess, IGoalDataAccess goalDataAccess)
    {
        this.userDataAccess = userDataAccess;
        this.goalDataAccess = goalDataAccess;
    }

    public async Task Execute(CreateGoalDto newGoal, string authUserId)
    {
        Guid userId = UtilsEntity.CastValidateId(authUserId);
        Console.WriteLine("[Info] Auth user id is valid");

        GoalEntity.ValidateGoal(newGoal);
        Console.WriteLine("[Info] New goal is valid");

        await UserEntity.FindUserById(userId, this.userDataAccess);
        Console.WriteLine("[Info] User found by auth user id");

        await GoalEntity.CreateGoal(newGoal, userId, this.goalDataAccess);
    }
}
