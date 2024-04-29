using System;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult HomePage()
    {
        var userId = Request.Cookies["userId"];
        if (string.IsNullOrWhiteSpace(userId)) {
            TempData["errorMessage"] = "You must be logged in to access this page";
            return RedirectToAction("SignInUserPage", "Pages");
        }

        SetMessagesToViewData();
        return View("~/Views/Index.cshtml");
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
        SetMessagesToViewData();
        return View("~/Views/SignInUser.cshtml");
    }

    [HttpGet("/Users/SignUp")]
    public IActionResult SignUpUserPage()
    {
        SetMessagesToViewData();
        return View("~/Views/SignUpUser.cshtml");
    }

    [HttpGet("/Posts/Show/{id}")]
    public IActionResult ShowPostPage(string id)
    {
        SetMessagesToViewData();
        return View("~/Views/ShowPost.cshtml");
    }

    [HttpGet("/Posts/List")]
    public IActionResult PostsListPage()
    {
        SetMessagesToViewData();
        return View("~/Views/ListPosts.cshtml");
    }

    [HttpGet("/Posts/Add")]
    public IActionResult AddPostPage()
    {
        SetMessagesToViewData();
        return View("~/Views/AddPost.cshtml");
    }

    [HttpGet("/Posts/Edit")]
    public IActionResult EditPostPage()
    {
        SetMessagesToViewData();
        return View("~/Views/EditPost.cshtml");
    }
}
