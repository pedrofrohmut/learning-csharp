using System;
using System.Threading.Tasks;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.Entities;

namespace Shareposts.Core.UseCases.Posts;

public class AddPostUseCase
{
    private readonly IUsersDataAccess usersDataAccess;
    private readonly IPostsDataAccess postsDataAccess;

    public AddPostUseCase(IUsersDataAccess usersDataAccess, IPostsDataAccess postsDataAccess)
    {
        this.usersDataAccess = usersDataAccess;
        this.postsDataAccess = postsDataAccess;
    }

    public async Task Execute(CreatePostDto newPost, string? userId)
    {
        var guidUserId = MainEntity.CastValidateId(userId);
        Console.WriteLine("[Info] User id is a valid Guid");

        PostEntity.ValidatePost(newPost);
        Console.WriteLine("[Info] Post is valid");

        var user = await UserEntity.CheckUserExistsById(guidUserId, this.usersDataAccess);
        Console.WriteLine("[Info] User with this id exists");

        await PostEntity.CreatePost(newPost, guidUserId, this.postsDataAccess);
        Console.WriteLine("[Info] Post created");
    }
}
