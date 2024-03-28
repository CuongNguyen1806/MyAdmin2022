using Microsoft.AspNetCore.Mvc;
using MyAdmin2022.Models;

namespace MyAdmin2022.Controllers
{
    public class ContactController : Controller
    {
        [Route("admin/lien-he", Name ="AdminContact")]
        public IActionResult Index()
        {
            DBContext db = new DBContext();
            var data = db.Contacts.OrderByDescending(x => x.CreateTime).Take(10).ToList();
            return View(data);
        }
    }
}
