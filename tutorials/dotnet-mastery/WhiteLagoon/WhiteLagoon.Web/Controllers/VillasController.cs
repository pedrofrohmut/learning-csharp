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
        if (villa.Name == villa.Description) {
            // Leave blank the be ModelOnly or provide a name if targeting a field
            ModelState.AddModelError("", "The description cannot be the same of the name");
        }
        if (!ModelState.IsValid) {
            return View();
        }
        this.dbContext.Add(villa);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Villas");
    }

    [HttpGet]
    public IActionResult Update(int villaId)
    {
        var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaId);
        if (villa == null) {
            return RedirectToAction("Error", "Home");
        }
        return View(villa);
    }

    [HttpPost]
    public IActionResult Update(Villa updatedVilla)
    {
        if (updatedVilla.Name == updatedVilla.Description) {
            // Leave blank the be ModelOnly or provide a name if targeting a field
            ModelState.AddModelError("Name", "The description cannot be the same of the name");
        }
        if (!ModelState.IsValid) {
            return View();
        }
        this.dbContext.Update(updatedVilla);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Villas");
    }

    [HttpGet]
    public IActionResult Delete(int villaId)
    {
        var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaId);
        if (villa == null) {
            return RedirectToAction("Error", "Home");
        }
        return View(villa);
    }


    [HttpPost]
    public IActionResult Delete(Villa villaToDelete)
    {
        var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaToDelete.Id);
        if (villa == null) {
            return RedirectToAction("Error", "Home");
        }
        this.dbContext.Villas?.Remove(villa);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Villas");
    }
}
