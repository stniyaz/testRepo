using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AllUpTag.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
