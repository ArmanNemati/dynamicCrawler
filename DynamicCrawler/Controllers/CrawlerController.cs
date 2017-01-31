using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using CrawlerLibrary.Helper;
using CrawlerLibrary.Models;
using DynamicCrawler.Helper;
using DynamicCrawler.Models;

namespace DynamicCrawler.Controllers
{
    public class CrawlerController : Controller
    {
        readonly DynamicCrawlerDBContext _db;
        public CrawlerController()
        {
            _db = new DynamicCrawlerDBContext();
        }


        public ActionResult Crawl(int mappingCode)
        {
            var mapping = _db.Mappings.Include(m => m.Urls).Include(m => m.Properties).SingleOrDefault(m => m.Id == mappingCode);
            List<RecordValueViewModel> result = Crawler.Crawl(Factory.Convert(mapping));
            return Json(result, JsonRequestBehavior.AllowGet);
            //return PartialView(result);
        }
    }
}