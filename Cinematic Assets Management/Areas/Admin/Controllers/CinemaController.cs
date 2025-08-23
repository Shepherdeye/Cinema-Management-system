using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CinemaController : Controller
    {
        private AppicationDbContext _context = new();
        public IActionResult Index()
        {
            var cinemas = _context.Cinemas.Include(e => e.Movies);
            return View(cinemas.ToList());
        }
        public IActionResult Related(int id)
        {
            var cinema = _context.Cinemas.Include(c => c.Movies).FirstOrDefault(e => e.Id == id);
            if (cinema is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);

            return View(cinema);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cinema cinema)
        {
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(e => e.Id == id);
            if(cinema is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);
            }

            return View(cinema);
        }

        public IActionResult Edit(Cinema cinema)
        {
            _context.Cinemas.Update(cinema);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Cinemas.FirstOrDefault(e => e.Id == id);
            if (category is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);


            _context.Cinemas.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
