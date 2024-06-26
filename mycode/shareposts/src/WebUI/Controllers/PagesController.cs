using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shareposts.Core.Adapters.Web;
using Shareposts.Core.UseCases.Posts;
using Shareposts.DataAccess;
using Shareposts.Services;
using Shareposts.Utils;
using Shareposts.WebUI.Utils;

namespace Shareposts.WebUI.Controllers;

[Route("/")]
public class PagesController : Controller
{
    private void SetMessagesToViewData()
    {
        ViewData["errorMessage"] = TempData["errorMessage"];
        ViewData["successMessage"] = TempData["successMessage"];
    }

    [HttpGet]
    [Route("/")]
    [Route("/Home")]
    public async Task<IActionResult> HomePage()
    {
        SetMessagesToViewData();

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            var connectionString = EnvUtils.GetConnectionString();
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            var postsDataAccess = new PostsDataAccess(connection);
            var listPostsUseCase = new ListPostsUseCase(postsDataAccess);

            var response = await PostsWebAdapter.ListPosts(listPostsUseCase);

            if (response.statusCode != 200) {
                ViewData["errorMessage"] = response.message;
            }

            return View("~/Views/Index.cshtml", new { posts = response.body });
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpGet("/About")]
    public IActionResult AboutPage()
    {
        SetMessagesToViewData();
        return View("~/Views/About.cshtml");
    }

    [HttpGet("/Users/SignIn")]
    public IActionResult SignInUserPage()
    {
        var (isNotAuthenticated, action, controller) =
            ControllerUtils.IsNotAuthenticatedOrRedirect(Request, TempData);
        if (! isNotAuthenticated) {
            return RedirectToAction(action, controller);
        }

        SetMessagesToViewData();
        return View("~/Views/SignInUser.cshtml");
    }

    [HttpGet("/Users/SignUp")]
    public IActionResult SignUpUserPage()
    {
        var (isNotAuthenticated, action, controller) =
            ControllerUtils.IsNotAuthenticatedOrRedirect(Request, TempData);
        if (! isNotAuthenticated) {
            return RedirectToAction(action, controller);
        }

        SetMessagesToViewData();
        return View("~/Views/SignUpUser.cshtml");
    }

    // [HttpGet("/Posts/Show/{id}")]
    // public IActionResult ShowPostPage(string id)
    // {
    //     SetMessagesToViewData();
    //     return View("~/Views/ShowPost.cshtml");
    // }

    [HttpGet("/Posts/CurrentUser")]
    public async Task<IActionResult> CurrentUserPostsPage()
    {
        var (isAuthenticated, action, controller) =
            ControllerUtils.IsAuthenticatedOrRedirect(Request, TempData);
        if (! isAuthenticated) {
            return RedirectToAction(action, controller);
        }

        SetMessagesToViewData();

        var connectionManager = new ConnectionManager();
        IDbConnection? connection = null;
        try {
            var connectionString = EnvUtils.GetConnectionString();
            connection = connectionManager.GetConnection(connectionString);
            connectionManager.OpenConnection(connection);

            var usersDataAccess = new UsersDataAccess(connection);
            var postsDataAccess = new PostsDataAccess(connection);
            var listCurrentUserPostsUseCase = new ListCurrentUserPostsUseCase(usersDataAccess, postsDataAccess);

            var jwtSecret = EnvUtils.GetJwtSecret();
            var jwtService = new JwtService(jwtSecret);
            var authUserId = await ControllerUtils.GetAuthUserIdFromRequest(Request, jwtService);

            var response = await PostsWebAdapter.ListCurrentUserPosts(listCurrentUserPostsUseCase, authUserId);

            if (response.statusCode != 200) {
                ViewData["errorMessage"] = response.message;
            }

            return View("~/Views/CurrentUserPosts.cshtml", new { posts = response.body });
        } finally {
            connectionManager.CloseConnection(connection);
        }
    }

    [HttpGet("/Posts/Add")]
    public IActionResult AddPostPage()
    {
        var (isAuthenticated, action, controller) =
            ControllerUtils.IsAuthenticatedOrRedirect(Request, TempData);
        if (! isAuthenticated) {
            return RedirectToAction(action, controller);
        }

        SetMessagesToViewData();
        return View("~/Views/AddPost.cshtml");
    }

    [HttpGet("/Posts/Edit/{postId}")]
    public IActionResult EditPostPage()
    {
        var (isAuthenticated, action, controller) =
            ControllerUtils.IsAuthenticatedOrRedirect(Request, TempData);
        if (! isAuthenticated) {
            return RedirectToAction(action, controller);
        }

        SetMessagesToViewData();
        return View("~/Views/EditPost.cshtml");
    }
}
