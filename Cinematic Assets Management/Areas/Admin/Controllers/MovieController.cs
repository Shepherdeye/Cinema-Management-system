using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class MovieController : Controller
    {
        private AppicationDbContext _context = new();
        public IActionResult Index()
        {
            var movies = _context.Movies.Include(e => e.Category).Include(e => e.Cinema);
            return View(movies.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {


            var categories = _context.Categories.ToList();
            var cinemas = _context.Cinemas.ToList();


            MovieWithData movieWithData = new()
            {
                Categories = categories,
                Cinemas = cinemas,
            };

            return View(movieWithData);
        }
        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile ImgUrl)
        {
            if (ImgUrl is not null && ImgUrl.Length > 0)
            {

                //name of the file  img
                var fileName=Guid.NewGuid().ToString()+Path.GetExtension(ImgUrl.FileName);
                //path  of the file  img 
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\visitor\\assets",fileName);

                using (var  stream=System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                movie.ImgUrl = fileName;

                _context.Movies.Add(movie);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }


            return BadRequest();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(e => e.Id == id);
            if (movie is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);
            }

            return View(movie);
        }

        public IActionResult Edit(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(e => e.Id == id);
            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);


            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
