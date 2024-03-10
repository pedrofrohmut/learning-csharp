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
	// Console.WriteLine("Category from post: {0}, {1}", category.Name, category.DisplayOrder);
        context.Categories.Add(category);
        context.SaveChanges();
        return RedirectToAction("Index", "Category");
    }
}
