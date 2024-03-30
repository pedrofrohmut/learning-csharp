using Microsoft.AspNetCore.Mvc;

namespace TodosMvc.Mvc.Controllers;

public class PagesController : Controller
{
    [HttpGet("/")]
    public IActionResult HomePage()
    {
        return View("~/pages/index.cshtml");
    }

    [HttpGet("/about")]
    public IActionResult AboutPage()
    {
        return View("~/pages/about.cshtml");
    }

    [HttpGet("/signup")]
    public IActionResult SignUpPage()
    {
        return View("~/pages/signup.cshtml");
    }

    [HttpGet("/signin")]
    public IActionResult SignInPage()
    {
        return View("~/pages/signin.cshtml");
    }

    [HttpGet("/signout")]
    public IActionResult SignOutPage()
    {
        return View("~/pages/index.cshtml");
    }
}
