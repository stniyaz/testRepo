using AllUpTag.DAL;
using AllUpTag.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllUpTag.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if(!ModelState.IsValid) return View(tag);
            if (_context.Tags.Any(t => t.Name.Trim().ToLower() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "alredy exist!!!");
                return View(tag);
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var wanted = _context.Tags.FirstOrDefault(t => t.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            var exist = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (exist == null) return NotFound();
            if(ModelState.IsValid) return View(tag);
            if (_context.Tags.Any(t => t.Id != tag.Id && t.Name.ToLower().Trim() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "alredy exist!!!");
                return View(tag);
            }
            exist.Name = tag.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _context.Tags.FirstOrDefault(_t => _t.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            var wanted = _context.Tags.FirstOrDefault(t=> t.Id == tag.Id);
            if (wanted == null) return NotFound();
            _context.Tags.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
