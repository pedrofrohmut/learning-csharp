using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.DataAccess;

public class PostsDataAccess : IPostsDataAccess
{
    private readonly IDbConnection connection;

    public PostsDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    public async Task CreatePost(CreatePostDto newPost, Guid userId)
    {
        var sql = "INSERT INTO posts (title, body, user_id) VALUES (@Title, @Body, @UserId)";
        await this.connection.ExecuteAsync(sql, new {
            @Title = newPost.title,
            @Body = newPost.body,
            @UserId = userId
        });
    }
}
