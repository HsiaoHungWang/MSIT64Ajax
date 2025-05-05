using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MSIT64Ajax.Models;

namespace MSIT64Ajax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
    

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
         
            return View();
        }

        public IActionResult JsonDemo()
        {
           
            return View();
        }

        public IActionResult Travel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(string userName)
        {
            System.Threading.Thread.Sleep(5000); //¼ÒÀÀ©µ¿ð
            ViewBag.Message = $"Hello {userName}" ;
            return View();
        }

        public IActionResult FirstAjax()
        {
            return View();
        }

        public IActionResult SecondAjax()
        {
            return View();
        }

        public IActionResult ThirdAjax()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Spots()
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
