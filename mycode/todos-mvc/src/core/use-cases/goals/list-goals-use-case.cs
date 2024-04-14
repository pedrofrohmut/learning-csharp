using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Entities;
using TodosMvc.Core.Dtos;

namespace TodosMvc.Core.UseCases.Goals;

public class ListGoalsUseCase
{
    private readonly IUserDataAccess userDataAccess;
    private readonly IGoalDataAccess goalDataAccess;

    public ListGoalsUseCase(IUserDataAccess userDataAccess, IGoalDataAccess goalDataAccess)
    {
        this.userDataAccess = userDataAccess;
        this.goalDataAccess = goalDataAccess;
    }

    public async Task<List<GoalDbDto>> Execute(string authUserId)
    {
        Guid validUserId = UtilsEntity.CastValidateId(authUserId);
        Console.WriteLine("[Info] Auth user id is valid");

        await UserEntity.CheckUserExists(validUserId, this.userDataAccess);
        Console.WriteLine("[Info] User with id equal to authUserId exists");

        var goals = await GoalEntity.ListGoals(validUserId, this.goalDataAccess);
        Console.WriteLine("[Info] Goals found and listed for the current authenticated user");

        if (goals.Count > 1) {
            Console.WriteLine("[Info] Goals are NOT empty for this user");
        }

        // if (goals.Count < 1) Console.WriteLine("The goals is EMPTY");
        // foreach (var x in goals) {
        //     Console.WriteLine("UseCase - Goal: " + x.text);
        // }

        return goals;
    }
}
