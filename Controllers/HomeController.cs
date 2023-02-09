using Company.IRepository;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Service> _serviceRepo;
        private readonly IRepository<About> _aboutRepo;

        public HomeController(ILogger<HomeController> logger, IRepository<Service> serviceRepo
            , IRepository<About> aboutRepo)
        {
            _logger = logger;
            _serviceRepo = serviceRepo;
            _aboutRepo = aboutRepo;
        }

        public IActionResult Index()
        {
            dynamic dy =new ExpandoObject();
            dy.Srevicelist = Srevice();
            dy.Aboutlist = About();


            return View(dy);
        }
        public List<Service> Srevice()
        {
            List<Service> srv = _serviceRepo.List().ToList();
            return srv;
        }
        public List<About> About()
        {
            List<About> abouts = _aboutRepo.List().ToList();
            return abouts;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}