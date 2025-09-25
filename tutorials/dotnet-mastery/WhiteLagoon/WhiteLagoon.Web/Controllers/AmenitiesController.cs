using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WhiteLagoon.Web.Controllers;

public class CreateAmenityModel
{
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 80 characters")]
    public string Name { get; set; } = "";
    [StringLength(250, ErrorMessage = "Description must not be longer then 250 characters")]
    public string? Description { get; set; } = "";
    [Range(1, int.MaxValue, ErrorMessage = "You must choose a villa")]
    public int VillaId { get; set; } = 0;
    public IEnumerable<Villa> Villas { get; set; } = new List<Villa>();
}

public class UpdateAmenityModel
{
    public int Id { get; set; }
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 80 characters")]
    public string Name { get; set; } = "";
    [StringLength(250, ErrorMessage = "Description must not be longer then 250 characters")]
    public string? Description { get; set; } = "";
    [Range(1, int.MaxValue, ErrorMessage = "You must choose a villa")]
    public int VillaId { get; set; }
    public string VillaName { get; set; } = "";
    public IEnumerable<Villa> OtherVillas = new List<Villa>();
}

public class AmenitiesController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public AmenitiesController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var amenities = this.dbContext.Amenities.ToList();
        return View("IndexAmenities", amenities);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var villas = this.dbContext.Villas.ToList();
        var model = new CreateAmenityModel { Villas = villas };
        return View("CreateAmenity", model);
    }

    [HttpPost]
    public IActionResult Create(CreateAmenityModel model)
    {
        if (!ModelState.IsValid) {
            var villas = this.dbContext.Villas.ToList();
            return View("CreateAmenity", new CreateAmenityModel { Villas = villas });
        }

        var villaExists = this.dbContext.Villas.Any(x => x.Id == model.VillaId);
        if (!villaExists) {
            TempData["error"] = "Villa not found with this id";
            var villas = this.dbContext.Villas.ToList();
            return View("CreateAmenity", new CreateAmenityModel { Villas = villas });
        }

        var newAmenity = new Amenity {
            Name = model.Name,
            Description = model.Description,
            VillaId = model.VillaId,
        };
        this.dbContext.Add(newAmenity);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Amenities");
    }

    [HttpGet]
    public IActionResult Update(int amenityId)
    {
        var amenity = this.dbContext.Amenities.Include(x => x.Villa).FirstOrDefault(x => x.Id == amenityId);
        if (amenity == null || amenity.Villa == null) {
            return RedirectToAction("Error", "Home");
        }
        var otherVillas = this.dbContext.Villas.Where(x => x.Id != amenity.VillaId).ToList();
        var model = new UpdateAmenityModel {
            Id = amenity.Id,
            Name = amenity.Name,
            Description = amenity.Description,
            VillaId = amenity.VillaId,
            VillaName = amenity.Villa.Name,
            OtherVillas = otherVillas,
        };
        return View("UpdateAmenity", model);
    }

    [HttpPost]
    public IActionResult Update(UpdateAmenityModel model)
    {
        if (!ModelState.IsValid) {
            return View("UpdateAmenity", model);
        }

        Utils.LogObject(model);

        var villaExists = this.dbContext.Villas.Any(x => x.Id == model.VillaId);
        var amenity = this.dbContext.Amenities.FirstOrDefault(x => x.Id == model.Id);
        if (!villaExists || amenity == null) {
            return RedirectToAction("Error", "Home");
        }

        amenity.Name = model.Name;
        amenity.Description = model.Description;
        amenity.VillaId = model.VillaId;
        this.dbContext.Update(amenity);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Amenities");
    }

    [HttpGet]
    public IActionResult Delete(int amenityId)
    {
        var amenity = this.dbContext.Amenities.Include(x => x.Villa).FirstOrDefault(x => x.Id == amenityId);
        if (amenity == null || amenity.Villa == null) {
            return RedirectToAction("Error", "Home");
        }
        return View("DeleteAmenity", amenity);
    }

    [HttpPost]
    public IActionResult Delete(Amenity model)
    {
        var amenityExists = this.dbContext.Amenities.Any(x => x.Id == model.Id);
        if (!amenityExists) {
            return RedirectToAction("Error", "Home");
        }
        this.dbContext.Remove(model);
        this.dbContext.SaveChanges();
        return RedirectToAction("Index", "Amenities");
    }
}
