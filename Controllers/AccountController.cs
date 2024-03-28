using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAdmin2022.Models;

namespace MyAdmin2022.Controllers
{
    public class AccountController : Controller
    {
        private IWebHostEnvironment Environment;
        public AccountController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        [Route("/", Name = "Default")]
        [Route("admin/login", Name = "AdminLogin")]
        public IActionResult Login()
        {
            ViewBag.LoginMessage = "Xin mời đăng nhập tài khoản";
            ViewBag.LoginClass = "alert-info";
            return View();
        }

        [HttpPost]
        [Route("admin/login", Name = "AdminLogin")]
        public IActionResult Login(Account data)
        {
            DBContext context = new DBContext();
            var item = context.Accounts.FirstOrDefault(x => x.Username == data.Username
                                                         && x.Password == data.Password);

            if (item == null)
            {
                ViewBag.LoginMessage = "Tài khoản không hợp lệ";
                ViewBag.LoginClass = "alert-danger";
                return View();
            }
            else
            {
                ViewBag.LoginMessage = "Đăng nhập thành công";
                ViewBag.LoginClass = "alert-success";
                ModelState.Clear();

                HttpContext.Session.SetString("Username", item.Username);
                HttpContext.Session.SetString("FullName", item.FullName ?? string.Empty);
                HttpContext.Session.SetString("Avatar", item.Avatar ?? string.Empty);

                return RedirectToRoute("AdminHome");
            }
        }

        [HttpPost]
        [Route("admin/logout", Name = "AdminLogout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("admin/tai-khoan", Name = "AdminAccount")]
        public IActionResult Index()
        {
            DBContext db = new DBContext();
            var data = db.Accounts
                         .OrderByDescending(x => x.CreateTime)
                         .Take(20)
                         .ToList();
            return View(data);
        }
    }
}