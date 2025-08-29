using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CategoryController : Controller
    {
        private AppicationDbContext _context = new();
        public IActionResult Index()
        {
            var categories = _context.Categories.Include(e => e.Movies);
            return View(categories.ToList());
        }
        public IActionResult Related(int id)
        {
            var category = _context.Categories.Include(c => c.Movies).FirstOrDefault(e => e.Id == id);
            if (category is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);

            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {          
            return View(new Category ());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage));
                TempData["error-notification"] = String.Join(", ",errors);
                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            TempData["success-notification"]="Added successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);
            if(category is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);
            }

            return View(category);
        }

        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["success-notification"] ="Updated Successfully";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);
            if (category is null)
                return RedirectToAction(SD.NotFoundPage, SD.AdminHomeController);


            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success-notification"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
