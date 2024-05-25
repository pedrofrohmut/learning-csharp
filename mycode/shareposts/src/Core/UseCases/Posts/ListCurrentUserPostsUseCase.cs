using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.ViewModels;
using Shareposts.Core.Entities;

namespace Shareposts.Core.UseCases.Posts;

public class ListCurrentUserPostsUseCase
{
    private readonly IUsersDataAccess usersDataAccess;
    private readonly IPostsDataAccess postsDataAccess;

    public ListCurrentUserPostsUseCase(IUsersDataAccess usersDataAccess, IPostsDataAccess postsDataAccess)
    {
        this.usersDataAccess = usersDataAccess;
        this.postsDataAccess = postsDataAccess;
    }

    public async Task<List<PostViewModel>> Execute(string? userId)
    {
        var guidUserId = MainEntity.CastValidateId(userId);
        Console.WriteLine("[Info] User id is valid and casted to Guid");

        await UserEntity.CheckUserExistsById(guidUserId, this.usersDataAccess);
        Console.WriteLine("[Info] User this id exists");

        var posts = await PostEntity.ListPostsWithAuthorByUserId(guidUserId, this.postsDataAccess);
        Console.WriteLine("[Info] Posts for the current user listed successfully");

        return posts;
    }
}
