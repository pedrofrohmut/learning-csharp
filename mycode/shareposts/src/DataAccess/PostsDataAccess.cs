using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.Db;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.DataAccess;

public class PostsDataAccess : IPostsDataAccess
{
    private readonly IDbConnection connection;

    public PostsDataAccess(IDbConnection connection)
    {
        this.connection = connection;
    }

    public PostDbDto MapPostRowToDto(dynamic postRow) => new PostDbDto () {
        id = postRow.id.ToString(),
        title = postRow.title,
        body = postRow.body,
        userId = postRow.user_id.ToString(),
        createdAt = postRow.created_at,
    };

    public List<PostDbDto> MapPostRowsToDtos(IEnumerable<dynamic> rows)
    {
        var list = new List<PostDbDto>();
        foreach (var row in rows) {
            var post = MapPostRowToDto(row);
            list.Add(post);
        }
        return list;
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

    public async Task<List<PostDbDto>> FindAllPosts()
    {
        var sql = "SELECT id, title, body, user_id, created_at FROM posts";
        var rows = await this.connection.QueryAsync(sql);
        var posts = this.MapPostRowsToDtos(rows);
        return posts;
    }
}
