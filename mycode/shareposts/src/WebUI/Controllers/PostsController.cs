using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shareposts.Core.Dtos.UseCases;
using Shareposts.WebUI.Utils;

namespace Shareposts.WebUI.Controllers;

[Route("Posts")]
public class PostsController : Controller
{
    [HttpPost("Add")]
    public async Task<IActionResult> AddPost()
    {
        var (isAuthenticated, action, controller) =
            await ControllerUtils.IsAuthenticatedWithTokenOrRedirect(Request, TempData);
        if (! isAuthenticated) {
            return RedirectToAction(action, controller);
        }

        var newPost = new CreatePostDto() {
            title = Request.Form["title"],
            body = Request.Form["body"]
        };

        Console.WriteLine("Title: " + newPost.title);
        Console.WriteLine("Body: " + newPost.body);

        return RedirectToAction("HomePage", "Pages");
    }
}
