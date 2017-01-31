﻿using System.Data.Entity;
using DynamicCrawler.Models;
using System.Linq;
using System.Web.Mvc;

namespace DynamicCrawler.Controllers
{
    public class MappingController : Controller
    {
        private readonly DynamicCrawlerDBContext _db;
        public MappingController()
        {
            _db = new DynamicCrawlerDBContext();
        }
        public ActionResult Index()
        {
            return View(_db.Mappings.ToList());
        }

        public ActionResult GetAll()
        {
            return Json(_db.Mappings.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Mapping mapping)
        {
            _db.Mappings.Add(mapping);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return PartialView(_db.Mappings.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Mapping mapping)
        {
            _db.Entry(mapping).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return PartialView(_db.Mappings.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(Mapping mapping)
        {
            _db.Entry(mapping).State = EntityState.Deleted;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return PartialView(_db.Mappings.Include(m => m.Properties).Include(m => m.Urls).SingleOrDefault(m => m.Id == id));
        }
    }
}