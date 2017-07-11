using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebArtSampler.Models;

namespace WebArtSampler.Controllers
{
    public class PatternMastersController : Controller
    {
        private ArtEntities db = new ArtEntities();

        // GET: PatternMasters
        public ActionResult Index()
        {
            return View(db.PatternMasters.ToList());
        }

        // GET: PatternMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternMaster patternMaster = db.PatternMasters.Find(id);
            if (patternMaster == null)
            {
                return HttpNotFound();
            }
            return View(patternMaster);
        }

        // GET: PatternMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatternMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatternMasterID,PaternMasterName,IsActive,Username")] PatternMaster patternMaster)
        {
            if (ModelState.IsValid)
            {
                db.PatternMasters.Add(patternMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patternMaster);
        }

        // GET: PatternMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternMaster patternMaster = db.PatternMasters.Find(id);
            if (patternMaster == null)
            {
                return HttpNotFound();
            }
            return View(patternMaster);
        }

        // POST: PatternMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatternMasterID,PaternMasterName,IsActive,Username")] PatternMaster patternMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patternMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patternMaster);
        }

        // GET: PatternMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternMaster patternMaster = db.PatternMasters.Find(id);
            if (patternMaster == null)
            {
                return HttpNotFound();
            }
            return View(patternMaster);
        }

        // POST: PatternMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PatternMaster patternMaster = db.PatternMasters.Find(id);
            db.PatternMasters.Remove(patternMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
