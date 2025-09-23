using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WhiteLagoon.Web.Controllers;

// It would work using just Villa. But with this model you pass less info to the view
// Also just to try out as an example
public class VillaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class CreateVillaNumberModel
{
    public VillaNumber VillaNumber { get; set; }
    public List<VillaModel> Villas { get; set; }

    public CreateVillaNumberModel()
    {
        this.VillaNumber = new VillaNumber();
        this.Villas = new List<VillaModel>();
    }

    public CreateVillaNumberModel(List<VillaModel>? villas)
    {
        this.VillaNumber = new VillaNumber();
        this.Villas = villas ?? new List<VillaModel>();
    }
}

public class VillaNumbersController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public VillaNumbersController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var villaNumbers = this.dbContext.VillaNumbers?.Include(x => x.Villa).ToList();
        return View(villaNumbers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var villaModels = this.dbContext.Villas
            .Select(x => new VillaModel { Id = x.Id, Name = x.Name })
            .ToList();
        return View(new CreateVillaNumberModel(villaModels));
    }

    [HttpPost]
    public IActionResult Create(CreateVillaNumberModel model)
    {
        // Check if villas selected
        if (model.VillaNumber.VillaId == 0) {
            TempData["error"] = "No villa selected";
            return RedirectToAction("Create", "VillaNumbers");
        }

        // Validate if the villa exists
        var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == model.VillaNumber.VillaId);
        if (villa == null) {
            TempData["error"] = "No villa registered with the passed id";
            return RedirectToAction("Create", "VillaNumbers");
        }

        // Check if number is already taken
        var existingVillaNumber = this.dbContext.VillaNumbers.FirstOrDefault(x => x.Number == model.VillaNumber.Number);
        if (existingVillaNumber != null) {
            TempData["error"] = "This number has already been taken";
            return RedirectToAction("Create", "VillaNumbers");
        }

        this.dbContext.Add(model.VillaNumber);
        this.dbContext.SaveChanges();

        TempData["success"] = "VillaNumber was created successfully";
        return RedirectToAction("Index", "VillaNumbers");
    }

    // [HttpGet]
    // public IActionResult Update(int villaId)
    // {
    //     var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaId);
    //     if (villa == null) {
    //         TempData["error"] = "Villa not found with this id";
    //         return RedirectToAction("Error", "Home");
    //     }
    //     return View(villa);
    // }

    // [HttpPost]
    // public IActionResult Update(Villa updatedVilla)
    // {
    //     if (updatedVilla.Name == updatedVilla.Description) {
    //         // Leave blank the be ModelOnly or provide a name if targeting a field
    //         ModelState.AddModelError("Name", "The description cannot be the same of the name");
    //     }
    //     if (!ModelState.IsValid) {
    //         return View();
    //     }
    //     if (updatedVilla.Id == 0) {
    //         TempData["error"] = "There is no id for the villa. Could not update model";
    //         return RedirectToAction("Error", "Home");
    //     }
    //     this.dbContext.Update(updatedVilla);
    //     this.dbContext.SaveChanges();
    //     TempData["success"] = "The villa was updated successfully";
    //     return RedirectToAction("Index", "Villas");
    // }

    // [HttpGet]
    // public IActionResult Delete(int villaId)
    // {
    //     var villa = this.dbContext.Villas?.FirstOrDefault(x => x.Id == villaId);
    //     if (villa == null) {
    //         TempData["error"] = "Villa not found with this id";
    //         return RedirectToAction("Error", "Home");
    //     }
    //     return View(villa);
    // }


    // [HttpPost]
    // public IActionResult Delete(Villa villaToDelete)
    // {
    //     this.dbContext.Villas?.Remove(villaToDelete);
    //     this.dbContext.SaveChanges();
    //     TempData["success"] = "The villas was deleted successfully";
    //     return RedirectToAction("Index", "Villas");
    // }
}
