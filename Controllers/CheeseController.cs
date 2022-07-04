using Microsoft.AspNetCore.Mvc;
using CheezeGallery.Models;
using CheezeGallery.Services;

namespace CheezeGallery.Controllers;

public class CheeseController : Controller
{
    public CheeseController(ILogger<HomeController> logger, ICheeseService cheeseService)
    {
        _logger = logger;
        _cheeseService = cheeseService;
    }

    private readonly ILogger<HomeController> _logger;
    private ICheeseService _cheeseService;

    public async Task<IActionResult> Index()
    {
        string query = "SELECT * FROM cheeses";
        IEnumerable<Cheese> cheeses = await _cheeseService.GetCheeses(query);
        return View(cheeses);
    }
}
