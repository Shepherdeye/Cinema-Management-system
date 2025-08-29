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

        public IActionResult Index(string? search = "", MovieFilter? filter = null, int page = 1)
        {
            var movies = _context.Movies.
             Include(e => e.Category).
             Include(e => e.Cinema).
             Include(e => e.ActorMovies)
             .ThenInclude(e => e.Actor).AsQueryable();

            if (search is not null && search.Length > 0)
            {
                ViewBag.Search = search;
                movies = movies.Where(e => e.Name.Contains(search) || e.Category.Name.Contains(search) || e.Description.Contains(search));
            }

            if (filter.Name is not null && filter.Name != "")
            {
                ViewBag.Name = filter.Name;
                movies = movies.Where(e => e.Name.Contains(filter.Name));
            }
            if (filter.Price is not null)
            {
                ViewBag.Price = filter.Price;
                movies = movies.Where(e => e.Price == filter.Price);
            }
            if (filter.MovieStatus is not null)
            {
                ViewBag.MovieStatus = filter.MovieStatus;
                movies = movies.Where(e => e.MovieStatus == filter.MovieStatus);

            }
            if (filter.Category is not null)
            {
                ViewBag.Category = filter.Category;
                movies = movies.Where(e => e.CategoryId == filter.Category);
            }
            if (filter.Cinema is not null)
            {
                ViewBag.Cinema = filter.Cinema;
                movies = movies.Where(e => e.CinemaId == filter.Cinema);
            }


            // For  pagnation 

            ViewBag.CurrentPage = page;

            int TotalCount = movies.Count();

            decimal PageNumbers = Math.Ceiling((decimal)TotalCount / 6);
            ViewBag.PageNumbers = PageNumbers;

            movies = movies.Skip((page - 1) * 6).Take(6);



            var categories = _context.Categories;
            var cinemas = _context.Cinemas;



            // for slider 
            var slider = _context.Movies.OrderByDescending(e => e.Id).Skip(0).Take(6);



            // all data of the index
            MovieWithData moviesWithData = new()
            {
                Movies = movies.ToList(),
                Categories = categories.ToList(),
                Cinemas = cinemas.ToList(),
                Slider = slider.ToList(),
            };





            return View(moviesWithData);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).Include(e=>e.Images).
                Include(e => e.ActorMovies).ThenInclude(e => e.Actor)
                .FirstOrDefault(e => e.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movies = _context.Movies.
                   Include(e => e.Category).
                   Include(e => e.Cinema).
                   Include(e => e.ActorMovies)
                   .ThenInclude(e => e.Actor).Where(e=>e.Id != id&& e.CategoryId==movie.CategoryId).Skip(0).Take(4);

            var categories = _context.Categories;

            var cinemas = _context.Cinemas;


            // all data of the index
            MovieWithData moviesWithData = new()
            {
                Movies = movies.ToList(),
                Categories = categories.ToList(),
                Cinemas = cinemas.ToList(),
                Movie = movie
            };

            return View(moviesWithData);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
