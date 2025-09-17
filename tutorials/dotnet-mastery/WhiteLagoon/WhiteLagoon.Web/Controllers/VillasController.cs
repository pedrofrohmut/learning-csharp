using WhiteLagoon.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace WhiteLagoon.Web.Controllers;

public class VillasController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public VillasController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var villas = this.dbContext.Villas?.ToList();
        return View(villas);
    }
}
