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

    public IActionResult Index()
    {
	var categories = context.Categories.ToList<CategoryModel>();
        return View(categories);
    }
}
