using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ActorController : Controller
    {
        private AppicationDbContext _context = new();
        public IActionResult Index()
        {
            var actors = _context.Actors.OrderByDescending(e => e.Id);
            return View(actors.ToList());
        }
        public IActionResult Related(int id)
        {
            var actor = _context.Actors.Include(c => c.ActorMovies).FirstOrDefault(e => e.Id == id);
            var actormovies = _context.ActorMovies.Include(e => e.Movie).Where(e => e.ActorId == id).ToList();
            var ActorsData = new ActorsData()
            {
                Actor = actor,
                ActorMovies = actormovies

            };
            if (actor is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);

            return View(ActorsData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Actor());
        }

        [HttpPost]
        public IActionResult Create(Actor actor, IFormFile ProfilePicture)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }



            if (ProfilePicture is not null && ProfilePicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets\\cast", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                actor.ProfilePicture = fileName;

                _context.Actors.Add(actor);
                _context.SaveChanges();

                TempData["success-notification"] = "Added successfully";
                return RedirectToAction(nameof(Index));

            }

            return BadRequest();

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _context.Actors.FirstOrDefault(e => e.Id == id);
            if (actor is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);
            }

            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile ProfilePicture)
        {
            var actorDb = _context.Actors.AsNoTracking().FirstOrDefault(e => e.Id == actor.Id);

            if (ProfilePicture is not null && ProfilePicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets\\cast", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                actor.ProfilePicture = fileName;

                var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\visitor\\assets\\cast", actorDb.ProfilePicture);
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete(oldpath);
                }

            }
            else
            {
                actor.ProfilePicture = actorDb.ProfilePicture;
            }

            _context.Actors.Update(actor);
            _context.SaveChanges();
            TempData["success-notification"] = "Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var actor = _context.Actors.FirstOrDefault(e => e.Id == id);
            if (actor is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);


            _context.Actors.Remove(actor);
            _context.SaveChanges();
            TempData["success-notification"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
