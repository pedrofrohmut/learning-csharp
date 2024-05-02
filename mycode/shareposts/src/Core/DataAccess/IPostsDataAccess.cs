using System;
using System.Threading.Tasks;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.Core.DataAccess;

public interface IPostsDataAccess
{
    Task CreatePost(CreatePostDto newPost, Guid userId);
}
