using Microsoft.AspNetCore.Mvc;
using MyAdmin2022.Models;

namespace MyAdmin2022.Controllers
{
    public class ProductController : Controller
    {
        [Route("admin/san-pham", Name ="AdminProduct")]
        public IActionResult Index()
        {
            DBContext db = new DBContext();
            var data = db.Accounts.OrderByDescending(x => x.CreateTime).Take(10).ToList();
            return View(data);
        }
    }
}
