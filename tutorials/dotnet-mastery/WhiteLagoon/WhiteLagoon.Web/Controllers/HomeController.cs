using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Application.Repositories;

namespace WhiteLagoon.Web.Controllers;

public class HomeModel
{
    public IEnumerable<Villa> Villas { get; set; } = new List<Villa>();
    public DateOnly CheckInDate { get; set; }
    public DateOnly CheckOutDate { get; set; }
    public uint NumberOfNights { get; set; } = 1;
}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly IVillaRepository villaRepository;

    public HomeController(ILogger<HomeController> logger, IVillaRepository villaRepository)
    {
        this.logger = logger;
        this.villaRepository = villaRepository;
    }

    public IActionResult Index()
    {
        var model = new HomeModel {
            Villas = this.villaRepository.FindAll(),
            NumberOfNights = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Now),
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}
