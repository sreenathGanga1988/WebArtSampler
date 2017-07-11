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
    public class SamplingFabricMastersController : Controller
    {
        private ArtEntities db = new ArtEntities();

        // GET: SamplingFabricMasters
        public ActionResult Index()
        {
            return View(db.SamplingFabricMasters.ToList());
        }

        // GET: SamplingFabricMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamplingFabricMaster samplingFabricMaster = db.SamplingFabricMasters.Find(id);
            if (samplingFabricMaster == null)
            {
                return HttpNotFound();
            }
            return View(samplingFabricMaster);
        }

        // GET: SamplingFabricMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SamplingFabricMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FabricID,Fabric,ISDeleted")] SamplingFabricMaster samplingFabricMaster)
        {
            if (ModelState.IsValid)
            {
                db.SamplingFabricMasters.Add(samplingFabricMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(samplingFabricMaster);
        }

        // GET: SamplingFabricMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamplingFabricMaster samplingFabricMaster = db.SamplingFabricMasters.Find(id);
            if (samplingFabricMaster == null)
            {
                return HttpNotFound();
            }
            return View(samplingFabricMaster);
        }

        // POST: SamplingFabricMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FabricID,Fabric,ISDeleted")] SamplingFabricMaster samplingFabricMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samplingFabricMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(samplingFabricMaster);
        }

        // GET: SamplingFabricMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamplingFabricMaster samplingFabricMaster = db.SamplingFabricMasters.Find(id);
            if (samplingFabricMaster == null)
            {
                return HttpNotFound();
            }
            return View(samplingFabricMaster);
        }

        // POST: SamplingFabricMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SamplingFabricMaster samplingFabricMaster = db.SamplingFabricMasters.Find(id);
            db.SamplingFabricMasters.Remove(samplingFabricMaster);
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
