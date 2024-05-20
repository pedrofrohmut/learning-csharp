using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shareposts.Core.Dtos.Db;
using Shareposts.Core.Dtos.UseCases;

namespace Shareposts.Core.DataAccess;

public interface IPostsDataAccess
{
    Task CreatePost(CreatePostDto newPost, Guid userId);
    Task<List<PostDbDto>> FindAllPosts();
    Task<List<PostWithUserDbDto>> FindAllPostsWithAuthor();
}
