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
    public class MerchandiserController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: Merchandiser
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ShowPendingDeliverytoAccept()
        {
            var samCutAssignmentMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster.MarkCompleted == null).Include(s => s.PatternMaster).Include(s => s.SampCutReqMaster);

            var samCutAssignmentMasterssort = samCutAssignmentMasters.ToList().OrderByDescending(a => a.ReceivedDate);

            return View(samCutAssignmentMasterssort.ToList());
        }


        public ActionResult AcceptDelivery(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamCutAssignmentMaster samCutAssignmentMaster = db.SamCutAssignmentMasters.Find(id);
            if (samCutAssignmentMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatternMasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName", samCutAssignmentMaster.PatternMasterID);
            ViewBag.SampCutreqID = new SelectList(db.SampCutReqMasters, "SampCutreqID", "ReqNum", samCutAssignmentMaster.SampCutreqID);
            return View(samCutAssignmentMaster);

        }
    }
}