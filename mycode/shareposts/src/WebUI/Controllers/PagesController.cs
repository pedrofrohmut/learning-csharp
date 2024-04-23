using Microsoft.AspNetCore.Mvc;

namespace Shareposts.WebUI.Controllers;

[Route("/")]
public class PagesController : Controller
{
    [HttpGet]
    [Route("/")]
    [Route("/Home")]
    public IActionResult HomePage()
    {
        return View("~/Views/Index.cshtml");
    }

    [HttpGet("/About")]
    public IActionResult AboutPage()
    {
        return View("~/Views/About.cshtml");
    }

    [HttpGet("/Users/SignIn")]
    public IActionResult SignInUserPage()
    {
        return View("~/Views/SignInUser.cshtml");
    }

    [HttpGet("/Users/SignUp")]
    public IActionResult SignUpUserPage()
    {
        return View("~/Views/SignUpUser.cshtml");
    }

    [HttpGet("/Posts/Show/{id}")]
    public IActionResult ShowPostPage(string id)
    {
        return View("~/Views/ShowPost.cshtml");
    }

    [HttpGet("/Posts/List")]
    public IActionResult PostsListPage()
    {
        return View("~/Views/ListPosts.cshtml");
    }

    [HttpGet("/Posts/Add")]
    public IActionResult AddPostPage()
    {
        return View("~/Views/AddPost.cshtml");
    }

    [HttpGet("/Posts/Edit")]
    public IActionResult EditPostPage()
    {
        return View("~/Views/EditPost.cshtml");
    }
}
