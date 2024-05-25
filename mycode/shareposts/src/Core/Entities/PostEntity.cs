using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shareposts.Core.DataAccess;
using Shareposts.Core.Dtos.Db;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.Dtos.ViewModels;
using Shareposts.Core.Exceptions;

namespace Shareposts.Core.Entities;

public static class PostEntity
{
    public static void ValidateTitle(string? title)
    {
        if (title == null) {
            throw new PostValidationException(
                "Title is null. Title is required and cannot be null.");
        }
        if (title.Length < 3) {
            throw new PostValidationException("Title is too short. Title must be at least 3 characters long.");
        }
        if (title.Length > 120) {
            throw new PostValidationException("Title is too long. Title must less than 120 characters long.");
        }
    }

    public static void ValidateBody(string? body)
    {
        if (body == null) {
            throw new PostValidationException("Body is null. Body is required and cannot be null.");
        }
        if (body.Length < 3) {
            throw new PostValidationException("Body is too short. Body must be at least 3 characters long.");
        }
        if (body.Length > 512) {
            throw new PostValidationException("Body is too long. Body must less than 512 characters long.");
        }
    }

    public static void ValidatePost(CreatePostDto newPost)
    {
        PostEntity.ValidateTitle(newPost.title);
        PostEntity.ValidateBody(newPost.body);
    }

    public static async Task CreatePost(CreatePostDto newPost, Guid userId, IPostsDataAccess postsDataAccess)
    {
        await postsDataAccess.CreatePost(newPost, userId);
    }

    public static async Task<List<PostDbDto>> ListPosts(IPostsDataAccess postsDataAccess)
    {
        return await postsDataAccess.FindAllPosts();
    }

    private static List<PostViewModel> MapPostWithUserDbDtoToPostViewModel(List<PostWithUserDbDto> postsDb)
    {
        var posts = new List<PostViewModel>();
        foreach (var postDb in postsDb) {
            var post = new PostViewModel() {
                id = postDb.id,
                title = postDb.title,
                body = postDb.body,
                createdAt = postDb.createdAt,
                authorId = postDb.authorId,
                authorName = postDb.authorName,
            };
            posts.Add(post);
        }
        return posts;
    }

    public static async Task<List<PostViewModel>> ListPostsWithAuthor(IPostsDataAccess postsDataAccess)
    {
        var postsDb = await postsDataAccess.FindAllPostsWithAuthor();
        return MapPostWithUserDbDtoToPostViewModel(postsDb);
    }

    public static async Task<List<PostViewModel>> ListPostsWithAuthorByUserId(Guid userId, IPostsDataAccess postsDataAccess)
    {
        var postsDb = await postsDataAccess.FindAllPostsWithAuthorByUserId(userId);
        return MapPostWithUserDbDtoToPostViewModel(postsDb);
    }
}
