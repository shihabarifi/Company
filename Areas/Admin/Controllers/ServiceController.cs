using Company.Data;
using Company.IRepository;
using Company.Models;
using Company.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly IRepository<Service> _serviceRepo;
        private readonly CompanyDbContext _context;

        public ServiceController(IRepository<Service> serviceRepo,CompanyDbContext context)
        {
            _serviceRepo = serviceRepo;
            _context = context;
        }

        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController
        public ActionResult Index()
        {
            return View(new ServiceVM
            {
                NewService = new NewService(),
                Services = _context.Services.OrderBy(x => x.Title).ToList()
            });
          
        }
        [Authorize(Roles = "Admin,User")]
        // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
            var srv = _serviceRepo.Find(id);
            return View(srv);
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
        public ActionResult Create(Service service)
        {
            try
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count() > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                    file[0].CopyTo(fileStream);
                    service.ImageUrl = ImageName;
                }
                else
                {
                    service.ImageUrl = service.ImageUrl;
                }
                _serviceRepo.Add(service);
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

            var srv = _serviceRepo.Find(id);
            return View(srv);
        }
        [Authorize(Roles = "Admin,User")]
        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service)
        {
            try
            {
                _serviceRepo.Update(service);
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
            var srv = _serviceRepo.Find(id);
            return View(srv);
        }
        [Authorize(Roles = "Admin,User")]
        // POST: ServiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Service service)
        {
            try
            {
                _serviceRepo.Delete(service.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
