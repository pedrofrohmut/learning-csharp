using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.ViewModels;
using Shareposts.Core.Entities;

namespace Shareposts.Core.UseCases.Posts;

public class ListPostsUseCase
{
    private readonly IPostsDataAccess postsDataAccess;

    public ListPostsUseCase(IPostsDataAccess postsDataAccess)
    {
        this.postsDataAccess = postsDataAccess;
    }

    public async Task<List<PostViewModel>> Execute()
    {
        var posts = await PostEntity.ListPostsWithAuthor(this.postsDataAccess);
        Console.WriteLine("[Info] Posts listed successfully");

        return posts;
    }
}
