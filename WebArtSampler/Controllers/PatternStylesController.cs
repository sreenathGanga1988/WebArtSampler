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
    public class PatternStylesController : Controller
    {
        private ArtEntities db = new ArtEntities();

        // GET: PatternStyles
        public ActionResult Index()
        {
            return View(db.PatternStyles.ToList());
        }

        // GET: PatternStyles/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternStyle patternStyle = db.PatternStyles.Find(id);
            if (patternStyle == null)
            {
                return HttpNotFound();
            }
            return View(patternStyle);
        }

        // GET: PatternStyles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatternStyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatternStyleID,StyleName")] PatternStyle patternStyle)
        {
            if (ModelState.IsValid)
            {
                db.PatternStyles.Add(patternStyle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patternStyle);
        }

        // GET: PatternStyles/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternStyle patternStyle = db.PatternStyles.Find(id);
            if (patternStyle == null)
            {
                return HttpNotFound();
            }
            return View(patternStyle);
        }

        // POST: PatternStyles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatternStyleID,StyleName")] PatternStyle patternStyle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patternStyle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patternStyle);
        }

        // GET: PatternStyles/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatternStyle patternStyle = db.PatternStyles.Find(id);
            if (patternStyle == null)
            {
                return HttpNotFound();
            }
            return View(patternStyle);
        }

        // POST: PatternStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PatternStyle patternStyle = db.PatternStyles.Find(id);
            db.PatternStyles.Remove(patternStyle);
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
