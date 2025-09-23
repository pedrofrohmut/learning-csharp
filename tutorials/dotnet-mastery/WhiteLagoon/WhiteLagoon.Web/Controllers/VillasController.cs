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
        TempData["success"] = "Villa was created successfully";
        return RedirectToAction("Index", "Villas");
    }

    [HttpGet]
    public IActionResult Update(int villaId)
    {
        var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaId);
        if (villa == null) {
            TempData["error"] = "Villa not found with this id";
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
        if (updatedVilla.Id == 0) {
            TempData["error"] = "There is no id for the villa. Could not update model";
            return RedirectToAction("Error", "Home");
        }
        this.dbContext.Update(updatedVilla);
        this.dbContext.SaveChanges();
        TempData["success"] = "The villa was updated successfully";
        return RedirectToAction("Index", "Villas");
    }

    [HttpGet]
    public IActionResult Delete(int villaId)
    {
        var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaId);
        if (villa == null) {
            TempData["error"] = "Villa not found with this id";
            return RedirectToAction("Error", "Home");
        }
        return View(villa);
    }


    [HttpPost]
    public IActionResult Delete(Villa villaToDelete)
    {
        this.dbContext.Villas?.Remove(villaToDelete);
        this.dbContext.SaveChanges();
        TempData["success"] = "The villas was deleted successfully";
        return RedirectToAction("Index", "Villas");
    }
}
