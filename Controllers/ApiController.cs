using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSIT64Ajax.Models;

namespace MSIT64Ajax.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext db;
        private readonly IWebHostEnvironment env;
        public ApiController(MyDBContext context, IWebHostEnvironment _env)
        {
            db = context;
            env = _env;
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

        //public IActionResult Register(string name, string email, int age=20)
        //public IActionResult Register(UserDTO _user)
        public IActionResult Register(Member _user, IFormFile Avatar)
       // public IActionResult Register()
        {
            //取得實際路徑
            string filePath = Path.Combine(env.WebRootPath, "images", Avatar.FileName);
            //檔案上傳
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                Avatar.CopyTo(stream);
            }

            //把檔案轉成二進位 
            using (var ms = new MemoryStream())
            {
                Avatar.CopyTo(ms);
                _user.FileData = ms.ToArray();
            }


            _user.FileName = Avatar.FileName;


            //新增
            db.Members.Add(_user);
            db.SaveChanges();

            //return Content($"Hello {_user.Name}, {_user.Age} 歲了，電子郵件是 {_user.Email}", "text/plain");
            return Content(filePath, "text/plain");
        }

    }
}
