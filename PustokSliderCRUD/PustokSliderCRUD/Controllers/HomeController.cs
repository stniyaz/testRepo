using Microsoft.AspNetCore.Mvc;
using PustokSliderCRUD.DAL;
using PustokSliderCRUD.Models;
using System.Diagnostics;

namespace PustokSliderCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

    }
}
