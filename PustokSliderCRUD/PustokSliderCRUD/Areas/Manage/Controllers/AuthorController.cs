using Microsoft.AspNetCore.Mvc;
using PustokSliderCRUD.DAL;
using PustokSliderCRUD.Models;

namespace PustokSliderCRUD.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if(!ModelState.IsValid) return View(author);
            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var wanted = _context.Authors.FirstOrDefault(a=> a.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Author author)
        {
            var existAuthor = _context.Authors.FirstOrDefault(x=> x.Id == author.Id);
            if(existAuthor == null) return NotFound();
            if (!ModelState.IsValid) return View(existAuthor);

            existAuthor.Fullname = author.Fullname;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) 
        {
            var wanted = _context.Authors.FirstOrDefault(a=> a.Id == id);
            if(wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            var wanted = _context.Authors.FirstOrDefault(a=> a.Id == author.Id);
            if (wanted == null) return NotFound();
            _context.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
