using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSIT64Ajax.Models;

namespace MSIT64Ajax.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext db;
        public ApiController(MyDBContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(10000); //模擬延遲
            string content = "Ajax 好!!"; //"<h2>Hello World!!</h2>";
            return Content(content, "text/plain", System.Text.Encoding.UTF8);

        }

        public IActionResult Index1(string userName)
        {
            System.Threading.Thread.Sleep(5000); //模擬延遲
            string content = $"Hello {userName}"; //"<h2>Hello World!!</h2>";
            return Content(content, "text/plain", System.Text.Encoding.UTF8);

        }

        //讀取所有城市
        public async Task<IActionResult> Cities()
        {
            var cities = await db.Addresses.Select(a => a.City).Distinct().ToListAsync();
            return Json(cities);
        }

        //根據城市讀取所有鄉鎮區
        public async Task<IActionResult> Sites(string city)
        {
            var sites = await db.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct().ToListAsync();
            return Json(sites);
        }

        //根據鄉鎮區讀取所有路名
        public async Task<IActionResult> Roads(string site)
        {
            var sites = await db.Addresses.Where(a => a.SiteId == site).Select(a => a.Road).Distinct().ToListAsync();
            return Json(sites);
        }

        public IActionResult Avatar(string fileName = "cat1.jpg")
        {
            return File($"~/images/{fileName}", "image/jpeg");
        }

        public async Task<IActionResult> Avatar1(int id)
        {
            var member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            byte[] img = member.FileData;
            if(img == null)
            {
                return NotFound();
            }
            return File(img, "image/jpeg");

        }
   
        public async Task<IActionResult> CheckAccount(string name)
        {
            var member =  await db.Members.AnyAsync(m => m.Name == name);        
            return Content(member.ToString(), "text/plain");
        }
    
    }
}
