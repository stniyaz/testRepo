using Microsoft.AspNetCore.Mvc;
using PustokSliderCRUD.Controllers;
using PustokSliderCRUD.DAL;
using PustokSliderCRUD.Models;
using PustokSliderCRUD.ViewModels;

namespace PustokSliderCRUD.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            if(!ModelState.IsValid) return View(book);
            if(!_context.Authors.Any(a=> a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found!!!");
                return View();
            }
            if (!_context.Genres.Any(g => g.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId","Genre not found!!!");
                return View();
            }
            var check = false;
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if(!_context.Tags.Any(t=> t.Id == tagId))
                    check= true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found!");
                return View(book);
            }
            else
            {
                if(book.TagIds != null)
                {
                    foreach(var tagId in book.TagIds)
                    {
                        BookTag bookTag = new BookTag
                        {
                            Book = book,
                            TagId = tagId
                        };
                        _context.BookTags.Add(bookTag);
                    }
                }
            }

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            Book wanted = _context.Books.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            var existBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existBook == null) return NotFound();
            if (!ModelState.IsValid) return View(book);
            if (!_context.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found!!!");
                return View();
            }
            if (!_context.Genres.Any(g => g.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not found!!!");
                return View();
            }

            var check = false;
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_context.Tags.Any(t => t.Id == tagId))
                        check = true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found!");
                return View(book);
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var tagId in book.TagIds)
                    {
                        BookTag bookTag = new BookTag
                        {
                            Book = book,
                            TagId = tagId
                        };
                        _context.BookTags.Add(bookTag);
                    }
                }
            }

            
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;
            existBook.Name = book.Name;
            existBook.Description = book.Description;
            existBook.CostPrice = book.CostPrice;
            existBook.SalePrice = book.SalePrice;
            existBook.DiscountedPrice = book.DiscountedPrice;
            existBook.Code = book.Code;
            existBook.IsAvailable = book.IsAvailable;
            existBook.Tax = book.Tax;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _context.Books.FirstOrDefault(b=> b.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            var wanted = _context.Books.FirstOrDefault(b => b.Id == book.Id);
            if (wanted == null) return NotFound();
            _context.Books.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
