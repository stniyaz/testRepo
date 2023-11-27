using AllUpTag.DAL;
using AllUpTag.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AllUpTag.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _contex;

        public ProductController(AppDbContext contex)
        {
            _contex = contex;
        }
        public IActionResult Index()
        {
            var products = _contex.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.Tags = _contex.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.Tags = _contex.Tags.ToList();
            if (!ModelState.IsValid) return View(product);
            var check = false;
            if (product.TagIds != null)
            {
                foreach (var tagId in product.TagIds)
                {
                    if (!_contex.Tags.Any(t => t.Id == tagId))
                        check = true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found");
                return View(product);
            }
            else
            {
                if (product.TagIds != null)
                {
                    foreach (var tagId in product.TagIds)
                    {
                        ProductTag productTag = new ProductTag
                        {
                            Product = product,
                            TagId = tagId
                        };
                        _contex.ProductTags.Add(productTag);
                    }
                }
            }
            _contex.Products.Add(product);
            _contex.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _contex.Products.FirstOrDefault(p => p.Id == id);
            if (wanted == null) return View(wanted);
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var wanted = _contex.Products.FirstOrDefault(p => p.Id == product.Id);
            if (wanted == null) return NotFound();
            _contex.Products.Remove(wanted); 
            _contex.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
