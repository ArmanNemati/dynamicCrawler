using DynamicCrawler.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace DynamicCrawler.Controllers
{
    public class PropertyController : Controller
    {
        readonly DynamicCrawlerDBContext _db;
        public PropertyController()
        {
            _db = new DynamicCrawlerDBContext();
        }
        public ActionResult Index()
        {
            return View(_db.Properties.Include(p=>p.Mapping).ToList());
        }

        public ActionResult Create(int mappingCode)
        {
            ViewBag.MappingCode = mappingCode;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Property property)
        {
            _db.Properties.Add(property);
            _db.SaveChanges();
            return Json(new {success=true},JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int id)
        {
            var property = _db.Properties.Find(id);
            ViewBag.MappingCode = property.MappingCode;
            return PartialView(property);
        }

        [HttpPost]
        public ActionResult Edit(Property property)
        {
            _db.Entry(property).State=EntityState.Modified;
            _db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var prop = _db.Properties.Find(id);
            return PartialView(prop);
        }

        [HttpPost]
        public ActionResult Delete(Property property)
        {
            _db.Entry(property).State = EntityState.Deleted;
            _db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            return PartialView(_db.Properties.Include(m => m.Mapping).SingleOrDefault(m => m.Id == id));
        }
    }
}