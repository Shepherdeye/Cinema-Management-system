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
            var actors = _context.Actors.ToList();
            List <int> actorsIds= new List<int>();
            Movie movie = new Movie();
           
            MovieWithData movieWithData = new()
            {
                Categories = categories,
                Cinemas = cinemas,
                Movie = movie,
                Actors = actors,
                ActorsIds=actorsIds
            };

            return View(movieWithData);
        }
        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile ImgUrl, List<IFormFile>? Images,List<int> Actors) 
        {
            if (!ModelState.IsValid)
            {
                var categories = _context.Categories.ToList();
                var cinemas = _context.Cinemas.ToList();

                MovieWithData movieWithData = new()
                {
                    Categories = categories,
                    Cinemas = cinemas,

                    Movie = movie,
                    ActorsIds=Actors
                };

                var errors = ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage));
                TempData["error-notification"] = String.Join(", ",errors);



                return View(movieWithData);
            }

            if (ImgUrl is not null && ImgUrl.Length > 0)
            {

                //name of the file  img
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                //path  of the file  img 
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                movie.ImgUrl = fileName;

                //save images
                //to but the string of each img in array and  path  the  array  to the Images table
                List<string> imagesNew = new List<string>();

                foreach (var img in Images)
                {
                    var imgName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets", imgName);
                    using (var stream2 = System.IO.File.Create(imgPath))
                    {
                        img.CopyTo(stream2);
                    }

                    imagesNew.Add(imgName);
                }

                _context.Movies.Add(movie);
                _context.SaveChanges();

                foreach (var img in imagesNew)
                {
                    _context.Images.Add(new Images { MovieId = movie.Id, ImgUrl=img});

                }
                foreach(var actor in Actors)
                {
                    _context.ActorMovies.Add(new ActorMovies { MovieId=movie.Id,ActorId=actor});
                }
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
                Movie = movie,
            };


            return View(movieWithData);
        }

        public IActionResult Edit(Movie movie, IFormFile? ImgUrl)
        {
            var movieDB = _context.Movies.AsNoTracking().FirstOrDefault(e => e.Id == movie.Id);
            if (movieDB is null)
            {
                return BadRequest();

            }

            if (ImgUrl is not null && ImgUrl.Length > 0)
            {

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets", fileName);

                using (var stream = System.IO.File.Create(filePath))
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
