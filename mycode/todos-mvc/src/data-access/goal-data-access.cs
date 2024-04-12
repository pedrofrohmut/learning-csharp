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
}
