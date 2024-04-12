using TodosMvc.Core.Dtos;

namespace TodosMvc.Core.DataAccess;

public interface IGoalDataAccess
{
    Task CreateGoal(CreateGoalDto newGoal, Guid userId);
}
