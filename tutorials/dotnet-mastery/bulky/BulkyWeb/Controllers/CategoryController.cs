using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext context;

    public CategoryController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
	var categories = context.Categories.ToList<CategoryModel>();
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
	return View(new CategoryModel());
    }

    [HttpPost]
    public IActionResult Create(CategoryModel category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
        return RedirectToAction("Index", "Category");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
	if (id == null || id == 0) return BadRequest();
        var category = context.Categories.Find(id);
	if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(CategoryModel category)
    {
	context.Categories.Update(category);
	context.SaveChanges();
	return RedirectToAction("Index", "Category");
    }
}
