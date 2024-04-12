using Microsoft.AspNetCore.Mvc;

namespace TodosMvc.Mvc.Controllers;

/*
  1. Other controllers are not supposed to call the view directly.
  2. Other controllers should only call RedirectToAction and let PagesController
  call the views
*/
public class PagesController : Controller
{
    [HttpGet("/")]
    public IActionResult HomePage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;

        // string? authUserId = HttpContext.Session.GetString("authUserId");
        // if (string.IsNullOrWhiteSpace(authUserId)) {
        //     TempData["errorMessage"] = "You need to be logged in to access this page";
        //     return RedirectToAction("SignInPage", "Pages");
        // }

        return View("~/pages/index.cshtml");
    }

    [HttpGet("/about")]
    public IActionResult AboutPage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;
        return View("~/pages/about.cshtml");
    }

    [HttpGet("/signup")]
    public IActionResult SignUpPage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;
        return View("~/pages/signup.cshtml");
    }

    [HttpGet("/signin")]
    public IActionResult SignInPage()
    {
        ViewData["errorMessage"] = TempData["errorMessage"] as string;
        ViewData["successMessage"] = TempData["successMessage"] as string;
        return View("~/pages/signin.cshtml");
    }
}
