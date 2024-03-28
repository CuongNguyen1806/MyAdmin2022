using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAdmin2022.Models;

namespace MyAdmin2022.Controllers
{
    public class ArticleController : Controller
    {
        private IWebHostEnvironment Environment;
        public ArticleController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [Route("admin/tin-tuc", Name = "AdminArticle")]
        public IActionResult Index()
        {
            DBContext db = new DBContext();
            var data = db.Articles
                         .Include(x => x.ArticleCategory)
                         .OrderByDescending(x => x.CreateTime)
                         .Take(20)
                         .ToList();
            return View(data);
        }

        [HttpGet]
        [Route("admin/chi-tiet-tin-tuc/{id?}", Name = "AdminArticleDetail")]
        public IActionResult Detail(int? id)
        {
            DBContext db = new DBContext();
            var category = db.ArticleCategories.OrderBy(x => x.Position).ToList();
            ViewBag.ArticleCategory = category;

            if (TempData["MessageText"] != null)
                ViewBag.MessageText = TempData["MessageText"];
            else
                ViewBag.MessageText = "Xin mời nhập thông tin";

            if (TempData["MessageClass"] != null)
                ViewBag.MessageClass = TempData["MessageClass"];
            else
                ViewBag.MessageClass = "alert-info";

            var item = db.Articles.Find(id);
            if (item == null)
                return View(new Article());
            else
                return View(item);
        }

        [HttpPost]
        [Route("admin/chi-tiet-tin-tuc/{id?}", Name = "AdminArticleDetail")]
        public IActionResult Detail(int? id, Article item)
        {
            DBContext db = new DBContext();

            if(!ValidateForm(item))
            {
                var category = db.ArticleCategories.OrderBy(x => x.Position).ToList();
                ViewBag.ArticleCategory = category;
                return View(item);
            }

            if (id > 0)
            {
                item.ArticleId = id.Value;
                var existItem = db.Articles.Find(id);
                if (existItem == null)
                    return View();

                existItem.ArticleCategoryId = item.ArticleCategoryId;
                existItem.Position = item.Position;
                existItem.Title = item.Title;
                existItem.Description = item.Description;
                existItem.Content = item.Content;

                if (item.Avatar != null && item.Avatar.StartsWith("data:image"))
                {
                    existItem.Avatar = SaveImage(item.Avatar);
                }
            }
            else
            {
                item.CreateTime = DateTime.Now;
                item.Status = true;
                if (item.Avatar != null && item.Avatar.StartsWith("data:image"))
                {
                    item.Avatar = SaveImage(item.Avatar);
                }
                db.Entry(item).State = EntityState.Added;
            }

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("admin/xoa-tin-tuc/{id}", Name = "AdminArticleDelete")]
        public IActionResult Delete(int id)
        {
            DBContext db = new DBContext();
            var item = db.Articles.Find(id);

            if (item != null)
            {
                db.Articles.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("admin/the-loai-tin-tuc", Name = "AdminArticleCategory")]
        public IActionResult Category()
        {
            DBContext db = new DBContext();
            var data = db.ArticleCategories.OrderBy(x => x.Position).ToList();
            return View(data);
        }

        private string SaveImage(string base64)
        {
            base64 = base64.Replace("data:image/jpeg;base64,", string.Empty);
            base64 = base64.Replace("data:image/jpg;base64,", string.Empty);
            base64 = base64.Replace("data:image/gif;base64,", string.Empty);
            base64 = base64.Replace("data:image/png;base64,", string.Empty);

            string rootFolder = this.Environment.WebRootPath;
            string fileName = Guid.NewGuid() + ".jpg";
            byte[] bytes = Convert.FromBase64String(base64);
            string folderSave = $"/FileUploads/Article/Avatar/{fileName}";
            string folderDownload = $"{rootFolder}/{folderSave}".Replace("/", "\\");
            System.IO.File.WriteAllBytes(folderDownload, bytes);
            return folderSave;
        }

        private bool ValidateForm(Article item)
        {
            if (item.ArticleCategoryId == null || item.ArticleCategoryId <= 0)
            {
                ViewBag.MessageText = "Vui lòng chọn 1 thể loại tin tức";
                ViewBag.MessageClass = "alert-warning";
                return false;
            }
            else if (string.IsNullOrEmpty(item.Title))
            {
                ViewBag.MessageText = "Vui lòng nhập tiêu đề tin tức";
                ViewBag.MessageClass = "alert-warning";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
