using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WhiteLagoon.Web.Controllers;

public class VillasController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public VillasController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var villas = this.dbContext.Villas?.ToList();
        return View(villas);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Villa villa)
    {
        if (!ModelState.IsValid) {
            return View();
        }
        this.dbContext.Add(villa);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Villas");
    }
}
