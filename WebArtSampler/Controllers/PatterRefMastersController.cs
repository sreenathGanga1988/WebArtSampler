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
    public class PatterRefMastersController : Controller
    {
        private ArtEntities db = new ArtEntities();

        // GET: PatterRefMasters
        public ActionResult Index()
        {
            return View(db.PatterRefMasters.ToList());
        }

        // GET: PatterRefMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatterRefMaster patterRefMaster = db.PatterRefMasters.Find(id);
            if (patterRefMaster == null)
            {
                return HttpNotFound();
            }
            return View(patterRefMaster);
        }

        // GET: PatterRefMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatterRefMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatternRefID,PatterRefNum,AddedBy,AddedDate")] PatterRefMaster patterRefMaster)
        {
            if (ModelState.IsValid)
            {
                patterRefMaster.AddedDate = DateTime.Now;
                db.PatterRefMasters.Add(patterRefMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patterRefMaster);
        }

        // GET: PatterRefMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatterRefMaster patterRefMaster = db.PatterRefMasters.Find(id);
            if (patterRefMaster == null)
            {
                return HttpNotFound();
            }
            return View(patterRefMaster);
        }

        // POST: PatterRefMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatternRefID,PatterRefNum,AddedBy,AddedDate")] PatterRefMaster patterRefMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patterRefMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patterRefMaster);
        }

        // GET: PatterRefMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatterRefMaster patterRefMaster = db.PatterRefMasters.Find(id);
            if (patterRefMaster == null)
            {
                return HttpNotFound();
            }
            return View(patterRefMaster);
        }

        // POST: PatterRefMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PatterRefMaster patterRefMaster = db.PatterRefMasters.Find(id);
            db.PatterRefMasters.Remove(patterRefMaster);
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
