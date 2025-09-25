using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WhiteLagoon.Web.Controllers;

public class CreateAmenityModel
{
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 80 characters")]
    public string Name { get; set; } = "";
    public string? Description { get; set; } = "";
    [Range(1, int.MaxValue, ErrorMessage = "You must choose a villa")]
    public int VillaId { get; set; } = 0;
    public IEnumerable<Villa> Villas { get; set; } = new List<Villa>();
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

    // TODO: Update get
    // TODO: Update post
    // TODO: Delete get
    // TODO: Delete post
}
