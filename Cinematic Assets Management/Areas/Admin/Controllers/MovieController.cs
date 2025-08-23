using Cinematic_Assets_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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


            var categories = _context.Categories.ToList();
            var cinemas = _context.Cinemas.ToList();


            MovieWithData movieWithData = new()
            {
                Categories = categories,
                Cinemas = cinemas,
                Movie= movie,
            };


          
            return View(movieWithData);
        }

        public IActionResult Edit(Movie movie,IFormFile? ImgUrl)
        {
            var movieDB = _context.Movies.AsNoTracking().FirstOrDefault(e=>e.Id==movie.Id);
            if (movieDB is null)
            {
                return BadRequest();

            }

            if (ImgUrl is not null && ImgUrl.Length > 0) {

                var fileName = Guid.NewGuid().ToString()+Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\visitor\\assets",fileName);

                using(var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                movie.ImgUrl = fileName;

                var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets", movieDB.ImgUrl);
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete(oldpath);
                }

            }
            else
            {
                movie.ImgUrl = movieDB.ImgUrl;                   
            }
            _context.Movies.Update(movie);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(e => e.Id == id);
            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);

            var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets", movie.ImgUrl);
            if (System.IO.File.Exists(oldpath))
            {
                System.IO.File.Delete(oldpath);
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
