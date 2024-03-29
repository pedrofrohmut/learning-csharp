using Microsoft.AspNetCore.Mvc;
using MvcCourse.Web.DataAccess.Repositories;
using MvcCourse.Web.Models;

namespace MvcCourse.Web.Controllers;

[Route("categories")]
public class CategoriesController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        var categories = CategoryRepository.FindAllCategories();
        return View("~/views/categories/index.cshtml", new { Categories = categories });
    }

    [HttpGet("edit/{id?}")]
    public IActionResult Edit([FromRoute] int id)
    {
        var model = CategoryRepository.GetCategoryById(id);
        return View("~/views/categories/edit.cshtml", model);
    }

    [HttpPost("edit")]
    public IActionResult Edit([FromForm] Category formData)
    {
        if (! ModelState.IsValid) {
            return View("~/views/categories/edit.cshtml", formData);
        }
        CategoryRepository.UpdateCategory(formData);
        return RedirectToAction("Index", "Categories");
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        return View("~/views/categories/add.cshtml");
    }

    [HttpPost("add")]
    public IActionResult Add([FromForm] Category formData)
    {
        if (! ModelState.IsValid) {
            return View("~/views/categories/add.cshtml");
        }
        CategoryRepository.AddCategory(formData);
        return RedirectToAction("Index", "Categories");
    }
}
