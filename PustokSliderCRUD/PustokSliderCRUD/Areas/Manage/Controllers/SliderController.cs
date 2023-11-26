using Microsoft.AspNetCore.Mvc;
using PustokSliderCRUD.Models;
using PustokSliderCRUD.DAL;
using PustokSliderCRUD.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PustokSliderCRUD.Areas.Manage.Controllers
{
    [Area("manage")]
    public class Slider : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public Slider(AppDbContext context, IWebHostEnvironment env) 
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<PustokSliderCRUD.Models.Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PustokSliderCRUD.Models.Slider slider)
        {
            if(!ModelState.IsValid) return View();
            if(slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile","upload only png or jpg(jpeg) file!");
                    return View();
                }
                if(slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "upload file witch is lower than 2 mb!");
                    return View();
                }
                slider.Image = Helper.FileSave(_env.WebRootPath, "manage/upload/sliders", slider.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "must be upload image file!");
                return View(slider);
            }
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Models.Slider wanted = _context.Sliders.FirstOrDefault(s=> s.Id==id);
            if(wanted==null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Models.Slider slider)
        {
            Models.Slider exsist = _context.Sliders.FirstOrDefault(x=> x.Id==slider.Id);
            if(exsist==null) return NotFound();
            if(!ModelState.IsValid) return View();
            if(slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "upload only png or jpg(jpeg) file!");
                    return View(slider);
                }
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "upload file witch is lower than 2 mb!");
                    return View(slider);
                }
                string deletePath = Path.Combine(_env.WebRootPath, "manage/upload/sliders", exsist.Image);
                if(System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                exsist.Image = Helper.FileSave(_env.WebRootPath, "manage/upload/sliders", slider.ImageFile);
            }
            exsist.LowerText = slider.LowerText;
            exsist.UpperText = slider.UpperText;
            exsist.RedirectText = slider.RedirectText;
            exsist.RedirectUrl = slider.RedirectUrl;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Models.Slider wanted = _context.Sliders.FirstOrDefault(s=>s.Id == id);
            if (wanted == null) return View(wanted);
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Models.Slider slider)
        {
            Models.Slider wanted = _context.Sliders.FirstOrDefault(x=> x.Id == slider.Id);
            if(wanted == null) return View(slider);
            string deletePath = Path.Combine(_env.WebRootPath, "manage/upload/sliders", wanted.Image);
            if(System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            _context.Sliders.Remove(wanted);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
