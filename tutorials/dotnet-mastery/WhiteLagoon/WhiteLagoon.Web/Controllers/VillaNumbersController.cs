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
    public VillaNumber VillaNumber { get; set; } = new VillaNumber { Number = 0 };
    public List<VillaModel> Villas { get; set; } = new List<VillaModel>();
}

public class UpdateVillaNumberModel
{
    public int OldVillaNumber { get; set; }
    public VillaNumber VillaNumber { get; set; } = new VillaNumber() { Number = 0 };
    public List<VillaModel> OtherVillas { get; set; } = new List<VillaModel>();
}

public class DeleteVillaNumberModel
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string SpecialDetails { get; set; } = "";
    public string VillaName { get; set; } = "";
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
        var villas = this.dbContext.Villas.Select(x => new VillaModel { Id = x.Id, Name = x.Name }).ToList();
        var viewModel = new CreateVillaNumberModel {
            VillaNumber = new VillaNumber() { Number = 0 },
            Villas = villas ?? new List<VillaModel>(),
        };
        return View(viewModel);
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

    [HttpGet]
    public IActionResult Update(int villaNumberId)
    {
        var villaNumber = this.dbContext.VillaNumbers.Include(x => x.Villa).FirstOrDefault(x => x.Id == villaNumberId);
        if (villaNumber == null) {
            TempData["error"] = "Villa number not found with the passed id";
            return RedirectToAction("Index", "VillaNumbers");
        }

        // Get other villas to populate the select with the other options
        var otherVillas = this.dbContext.Villas.Where(x => x.Id != villaNumber.VillaId)
            .Select(x => new VillaModel { Id = x.Id, Name = x.Name }).ToList();

        var viewModel = new UpdateVillaNumberModel {
            OldVillaNumber = villaNumber.Number,
            VillaNumber = villaNumber,
            OtherVillas = otherVillas ?? new List<VillaModel>(),
        };
        return View("UpdateVillaNumber", viewModel);
    }

    [HttpPost]
    public IActionResult Update(UpdateVillaNumberModel model)
    {
        // Check villa exists
        var villaExists = this.dbContext.Villas.Any(x => x.Id == model.VillaNumber.VillaId);
        if (!villaExists) {
            TempData["error"] = "Villa with this Id does not exist";
            return RedirectToAction("Update", "VillaNumbers", new { villaNumberIs = model.VillaNumber.Id });
        }

        // Check number is available only if it changed
        if (model.OldVillaNumber != model.VillaNumber.Number) {
            var numberTaken = this.dbContext.VillaNumbers.Any(x => x.Number == model.VillaNumber.Number);
            if (numberTaken) {
                TempData["error"] = "Villa number already taken";
                return RedirectToAction("Update", "VillaNumbers", new { villaNumberId = model.VillaNumber.Id });
            }
        }

        this.dbContext.Update(model.VillaNumber);
        this.dbContext.SaveChanges();
        TempData["success"] = "The villa numbers was updated successfully";
        return RedirectToAction("Index", "VillaNumbers");
    }

    [HttpGet]
    public IActionResult Delete(int villaNumberId)
    {
        var villaNumber = this.dbContext.VillaNumbers.Include(x => x.Villa).FirstOrDefault(x => x.Id == villaNumberId);
        if (villaNumber == null) {
            TempData["error"] = "Villa Number not found with this id";
            return RedirectToAction("Error", "Home");
        }

        var viewModel = new DeleteVillaNumberModel {
            Id = villaNumber.Id,
            Number = villaNumber.Number,
            SpecialDetails = villaNumber.SpecialDetails ?? "",
            VillaName = villaNumber.Villa?.Name ?? "",
        };
        return View("DeleteVillaNumber", viewModel);
    }

    [HttpPost]
    public IActionResult Delete(DeleteVillaNumberModel model)
    {
        var exists = this.dbContext.VillaNumbers.Any(x => x.Id == model.Id);
        if (!exists) {
            TempData["error"] = "No villa number found by the passed id";
            return RedirectToAction("Index", "VillaNumbers");
        }

        this.dbContext.VillaNumbers.Remove(new VillaNumber { Id = model.Id, Number = 0 });
        this.dbContext.SaveChanges();
        TempData["success"] = "The villa number was deleted successfully";
        return RedirectToAction("Index", "VillaNumbers");
    }
}
