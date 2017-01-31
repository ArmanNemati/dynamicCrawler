using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DynamicCrawler.Models;

namespace DynamicCrawler.Controllers
{
    public class UrlController : Controller
    {
        readonly DynamicCrawlerDBContext _db;
        public UrlController()
        {
            _db = new DynamicCrawlerDBContext();
        }
        public ActionResult Index()
        {
            return View(_db.Urls.Include(u=>u.Mapping).ToList());
        }

        public ActionResult Create(int id) 
        {
            ViewBag.MappingCode = id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Url url)
        {
            _db.Urls.Add(url);
            _db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            var url = _db.Urls.Find(id);
            return PartialView(url);
        }

        [HttpPost]
        public ActionResult Edit(Url url)
        {
            _db.Entry(url).State = EntityState.Modified;
            _db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var url = _db.Urls.Find(id);
            return PartialView(url);
        }

        [HttpPost]
        public ActionResult Delete(Url url) 
        {
            _db.Entry(url).State = EntityState.Deleted;
            _db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}