using TodosMvc.Core.Dtos;

namespace TodosMvc.Core.DataAccess;

public interface IGoalDataAccess
{
    Task CreateGoal(CreateGoalDto newGoal, Guid userId);
    Task<List<GoalDbDto>> FindAllGoalsByUserId(Guid userId);
    Task<GoalDbDto?> FindGoalById(Guid goalId);
    Task DeleteGoal(Guid goalId);
}
