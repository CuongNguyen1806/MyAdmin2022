using Microsoft.AspNetCore.Mvc;

namespace MyAdmin2022.Controllers
{
    public class HomeController : Controller
    {
        [Route("admin/", Name="AdminHome")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
