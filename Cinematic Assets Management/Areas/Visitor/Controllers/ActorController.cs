using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.Areas.Visitor.Controllers
{
    [Area(SD.VisitorArea)]
    public class ActorController : Controller
    {
        private AppicationDbContext _context = new();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id )
        {
            var actor = _context.Actors.Include(e => e.ActorMovies).ThenInclude(e => e.Movie).FirstOrDefault(e=>e.Id==id);

            return View(actor);
        }
    }
}
