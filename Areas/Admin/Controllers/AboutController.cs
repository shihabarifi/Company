using Company.IRepository;
using Company.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutController : Controller
    {
        private readonly IRepository<About> _aboutRepo;

        public AboutController(IRepository<About> aboutRepo)
        {
            _aboutRepo = aboutRepo;
        }

        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController
        public ActionResult Index()
        {
            var abo = _aboutRepo.List();
            return View(abo);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
            var abo = _aboutRepo.Find(id);
            return View(abo);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,User")]
        // POST: ServiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(About about)
        {
            try
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count() > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                    file[0].CopyTo(fileStream);
                    about.ImageUrl = ImageName;
                }
                else
                {
                    about.ImageUrl = about.ImageUrl;
                }
                _aboutRepo.Add(about);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController/Edit/5
        public ActionResult Edit(int id)
        {

            var about = _aboutRepo.Find(id);
            return View(about);
        }
        [Authorize(Roles = "Admin,User")]
        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(About about)
        {
            try
            {
                _aboutRepo.Update(about);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            var about = _aboutRepo.Find(id);
            return View(about);
        }
        [Authorize(Roles = "Admin,User")]
        // POST: ServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(About about)
        {
            try
            {
                _aboutRepo.Delete(about.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
