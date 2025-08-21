using System.Diagnostics;
using Cinematic_Assets_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.Areas.Visitor.Controllers
{
    [Area(SD.VisitorArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppicationDbContext _context = new();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.Include(e=>e.Cinema).Include(e=>e.ActorMovies).ThenInclude(e=>e.Actor);

            return View(movies.ToList());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
