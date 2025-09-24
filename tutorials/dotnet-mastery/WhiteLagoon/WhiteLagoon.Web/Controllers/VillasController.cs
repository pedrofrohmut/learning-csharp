using WhiteLagoon.Application.Repositories;
using WhiteLagoon.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WhiteLagoon.Web.Controllers;

public class VillasController : Controller
{
    private readonly IVillaRepository villaRepository;
    private readonly IWebHostEnvironment webHostEnvironment;

    public VillasController(IVillaRepository villaRepository, IWebHostEnvironment webHostEnvironment)
    {
        this.villaRepository = villaRepository;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var villas = this.villaRepository.FindAll();
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

        if (villa.Image == null) {
            villa.ImageUrl = "https://placehold.co/600x400";
        } else {
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(villa.Image.FileName);
            string imgPath = Path.Combine(this.webHostEnvironment.WebRootPath, "images/villas", filename);
            using var f = new FileStream(imgPath, FileMode.Create);
            villa.Image.CopyTo(f);
            villa.ImageUrl = "/images/villas/" + filename;
        }

        this.villaRepository.Create(villa);
        this.villaRepository.Save();
        TempData["success"] = "Villa was created successfully";
        return RedirectToAction("Index", "Villas");
    }

    [HttpGet]
    public IActionResult Update(int villaId)
    {
        var villa = this.villaRepository.FindById(villaId);
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

        if (updatedVilla.Image != null && updatedVilla.ImageUrl != null) {
            string oldfilePath = updatedVilla.ImageUrl.Substring(1);
            string fullPath = Path.Combine(this.webHostEnvironment.WebRootPath, oldfilePath);

            if (System.IO.File.Exists(fullPath)) {
                System.IO.File.Delete(fullPath);
            }

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(updatedVilla.Image.FileName);
            string imgPath = Path.Combine(this.webHostEnvironment.WebRootPath, "images/villas", filename);
            using var f = new FileStream(imgPath, FileMode.Create);
            updatedVilla.Image.CopyTo(f);
            updatedVilla.ImageUrl = "/images/villas/" + filename;
        }

        this.villaRepository.Update(updatedVilla);
        this.villaRepository.Save();
        TempData["success"] = "The villa was updated successfully";
        return RedirectToAction("Index", "Villas");
    }

    [HttpGet]
    public IActionResult Delete(int villaId)
    {
        var villa = this.villaRepository.FindById(villaId);
        if (villa == null) {
            TempData["error"] = "Villa not found with this id";
            return RedirectToAction("Error", "Home");
        }
        return View(villa);
    }


    [HttpPost]
    public IActionResult Delete(Villa villaToDelete)
    {
        this.villaRepository.Remove(villaToDelete.Id);
        this.villaRepository.Save();
        TempData["success"] = "The villas was deleted successfully";
        return RedirectToAction("Index", "Villas");
    }
}
