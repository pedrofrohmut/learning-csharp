using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shareposts.Core.Adapters.Web;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.Core.UseCases.Posts;
using Shareposts.DataAccess;
using Shareposts.Utils;
using Shareposts.WebUI.Utils;

namespace Shareposts.WebUI.Controllers;

[Route("Posts")]
public class PostsController : Controller
{
    [HttpPost("Add")]
    public async Task<IActionResult> AddPost()
    {
        var (isAuthenticated, action, controller, userId) =
            await ControllerUtils.IsAuthenticatedWithTokenOrRedirect(Request, TempData);
        if (! isAuthenticated) {
            return RedirectToAction(action, controller);
        }

        var newPost = new CreatePostDto() {
            title = Request.Form["title"],
            body = Request.Form["body"]
        };

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            var connectionString = EnvUtils.GetConnectionString();
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            var usersDataAccess = new UsersDataAccess(connection);
            var postsDataAccess = new PostsDataAccess(connection);
            var addPostUseCase = new AddPostUseCase(usersDataAccess, postsDataAccess);

            var response = await PostsWebAdapter.AddPost(addPostUseCase, newPost, userId);

            if (response.statusCode != 201) {
                TempData["errorMessage"] = response.message;
                return RedirectToAction("AddPostPage", "Pages");
            }

            return RedirectToAction("HomePage", "Pages");
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }
}
