using System;
using System.Threading.Tasks;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.Dtos;
using Shareposts.Core.Exceptions;
using Shareposts.Core.UseCases.Posts;

namespace Shareposts.Core.Adapters.Web;

public static class PostsWebAdapter
{
    public static async Task<AdaptedWebResponse> AddPost(
        AddPostUseCase useCase, CreatePostDto newPost, string? userId)
    {
        try {
            await useCase.Execute(newPost, userId);
            return new AdaptedWebResponse() { statusCode = 201 };
        } catch (Exception e) {
            if (e is UserValidationException || e is UserNotFoundException) {
                return new AdaptedWebResponse() { statusCode = 403, message = e.Message };
            }
            if (e is PostValidationException) {
                return new AdaptedWebResponse() { statusCode = 400, message = e.Message };
            }
            throw;
        }
    }

    public static async Task<AdaptedWebResponse> ListPosts(ListPostsUseCase useCase)
    {
        var posts = await useCase.Execute();
        return new AdaptedWebResponse() { statusCode = 200, body = posts };
    }

    public static async Task<AdaptedWebResponse> ListCurrentUserPosts(ListCurrentUserPostsUseCase useCase, string? userId)
    {
        try {
            var posts = await useCase.Execute(userId);
            return new AdaptedWebResponse() { statusCode = 200, body = posts };
        } catch (Exception e) {
            if (e is UserValidationException || e is UserNotFoundException) {
                return new AdaptedWebResponse() { statusCode = 403, message = e.Message };
            }
            throw;
        }
    }
}
