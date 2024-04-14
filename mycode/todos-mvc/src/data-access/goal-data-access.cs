using System.Data;
using TodosMvc.Core.DataAccess;
using TodosMvc.Core.Dtos;
using Dapper;

namespace TodosMvc.DataAccess;

public class GoalDataAccess : IGoalDataAccess
{
    private readonly IDbConnection connection;

    public GoalDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    public async Task CreateGoal(CreateGoalDto newGoal, Guid userId)
    {
        var sql = "INSERT INTO goals (text, user_id) VALUES (@Text, @UserId)";
        await this.connection.ExecuteAsync(sql, new { Text = newGoal.content, UserId = userId });
    }

    public async Task<List<GoalDbDto>> FindAllGoalsByUserId(Guid userId)
    {
        var sql = "SELECT id, text, user_id, created_at FROM goals WHERE user_id = @UserId";
        var goalsRows = await this.connection.QueryAsync(sql, new { UserId = userId });
        var goals = new List<GoalDbDto>();
        if (goalsRows != null) {
            foreach (var row in goalsRows) {
                var goal = new GoalDbDto() {
                    id = row.id,
                    text = row.text,
                    userId = row.user_id,
                    createdAt = row.created_at,
                };
                goals.Add(goal);
            }
        }
        return goals;
    }
}
