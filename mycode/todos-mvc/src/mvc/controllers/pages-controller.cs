using Microsoft.AspNetCore.Mvc;

namespace TodosMvc.Mvc.Controllers;

public class PagesController : Controller
{
    [HttpGet("/")]
    public IActionResult HomePage()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"] as string;
        ViewData["SuccessMessage"] = TempData["SuccessMessage"] as string;
        return View("~/pages/index.cshtml");
    }

    [HttpGet("/about")]
    public IActionResult AboutPage()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"] as string;
        ViewData["SuccessMessage"] = TempData["SuccessMessage"] as string;
        return View("~/pages/about.cshtml");
    }

    [HttpGet("/signup")]
    public IActionResult SignUpPage()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"] as string;
        ViewData["SuccessMessage"] = TempData["SuccessMessage"] as string;
        return View("~/pages/signup.cshtml");
    }

    [HttpGet("/signin")]
    public IActionResult SignInPage()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"] as string;
        ViewData["SuccessMessage"] = TempData["SuccessMessage"] as string;
        return View("~/pages/signin.cshtml");
    }

    [HttpGet("/signout")]
    public IActionResult SignOutPage()
    {
        ViewData["ErrorMessage"] = TempData["ErrorMessage"] as string;
        ViewData["SuccessMessage"] = TempData["SuccessMessage"] as string;
        return View("~/pages/index.cshtml");
    }
}
