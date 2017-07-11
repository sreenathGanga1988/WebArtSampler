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
    public class SamCutAssignmentMastersController : Controller
    {
        private ArtEntities db = new ArtEntities();

        // GET: SamCutAssignmentMasters
        public ActionResult Index()
        {
            var samCutAssignmentMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster.MarkCompleted==null).Include(s => s.PatternMaster).Include(s => s.SampCutReqMaster);

           var samCutAssignmentMasterssort = samCutAssignmentMasters.ToList().OrderByDescending(a => a.ReceivedDate);
           


            foreach(var element in samCutAssignmentMasterssort)
            {


                element.TotalCut = 0;

                int SizeQty1 = element.SampCutReqMaster.Size1CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1CutQty.ToString());
                int SizeQty2 = element.SampCutReqMaster.Size2CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2CutQty.ToString());
                int SizeQty3 = element.SampCutReqMaster.Size3CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3CutQty.ToString());
                int SizeQty4 = element.SampCutReqMaster.Size4CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4CutQty.ToString());
                int SizeQty5 = element.SampCutReqMaster.Size5CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5CutQty.ToString());
                int SizeQty6 = element.SampCutReqMaster.Size6CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6CutQty.ToString());

                element.TotalCut = SizeQty1 + SizeQty2 + SizeQty3 + SizeQty4 + SizeQty5 + SizeQty6;



                int sewQty1 = element.SampCutReqMaster.Size1SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1SewQty.ToString());
                int sewQty2 = element.SampCutReqMaster.Size2SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2SewQty.ToString());
                int sewQty3 = element.SampCutReqMaster.Size3SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3SewQty.ToString());
                int sewQty4 = element.SampCutReqMaster.Size4SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4SewQty.ToString());
                int sewQty5 = element.SampCutReqMaster.Size5SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5SewQty.ToString());
                int sewQty6 = element.SampCutReqMaster.Size6SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6SewQty.ToString());

                element.TotalSew = sewQty1 + sewQty2 + sewQty3 + sewQty4 + sewQty5 + sewQty6;


                int deliveredQty1 = element.SampCutReqMaster.Size1DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1DeliveredQty.ToString());
                int deliveredQty2 = element.SampCutReqMaster.Size2DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2DeliveredQty.ToString());
                int deliveredQty3 = element.SampCutReqMaster.Size3DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3DeliveredQty.ToString());
                int deliveredQty4 = element.SampCutReqMaster.Size4DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4DeliveredQty.ToString());
                int deliveredQty5 = element.SampCutReqMaster.Size5DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5DeliveredQty.ToString());
                int deliveredQty6 = element.SampCutReqMaster.Size6DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6DeliveredQty.ToString());

                element.TotalDeliver = deliveredQty1 + deliveredQty2 + deliveredQty3 + deliveredQty4 + deliveredQty5 + deliveredQty6;






            }




            return View(samCutAssignmentMasterssort.ToList());
        }

        // GET: SamCutAssignmentMasters/Details/5
        public ActionResult Details(decimal id)
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
            return View(samCutAssignmentMaster);
        }

        // GET: SamCutAssignmentMasters/Create
        public ActionResult Create()
        {
            var sampCutReqMasters = db.SampCutReqMasters.Where(s=>s.IsReceived=="N" ).Include(s => s.BuyerMaster).Include(s => s.PatternStyle).Include(s => s.PatterRefMaster).Include(s => s.SampleType);


            var sampCutReqMasterssort = sampCutReqMasters.ToList().OrderByDescending(a => a.AddedDate);
            ViewBag.sampCutReqMasterssort= sampCutReqMasterssort.ToList();

            ViewBag.PatternMasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName");
            ViewBag.SampCutreqID = new SelectList(db.SampCutReqMasters. Where(s => s.IsReceived == "N"), "SampCutreqID", "ReqNum");
            return View();
        }

        // POST: SamCutAssignmentMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CutAssignID,SampCutreqID,ReceivedDate,ReceivedBy,PatternMasterID,PatternReqDate,SignedBYMaster,SignedDate,CompletedDate,Remark,MarkedCompletedBY")] SamCutAssignmentMaster samCutAssignmentMaster)
        {
            if (ModelState.IsValid)
            {
                samCutAssignmentMaster.SignedBYMaster = false;
                samCutAssignmentMaster.CompletedQty = 0;
                db.SamCutAssignmentMasters.Add(samCutAssignmentMaster);


                SampCutReqMaster sampCutReqMaster = db.SampCutReqMasters.Find(samCutAssignmentMaster.SampCutreqID);
                sampCutReqMaster.IsReceived = "Y";


                db.SaveChanges();
                

                return RedirectToAction("Index");
            }

            ViewBag.PatternMasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName", samCutAssignmentMaster.PatternMasterID);
            ViewBag.SampCutreqID = new SelectList(db.SampCutReqMasters, "SampCutreqID", "ReqNum", samCutAssignmentMaster.SampCutreqID);
            return View(samCutAssignmentMaster);
        }

        // GET: SamCutAssignmentMasters/Edit/5
        public ActionResult Edit(decimal id)
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

        // POST: SamCutAssignmentMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CutAssignID,SampCutreqID,ReceivedDate,ReceivedBy,PatternMasterID,AssignedDate,SignedBYMaster,SignedDate,CompletedDate,Remark,MarkedCompletedBY,PatternReqDate,PatternCompletedDate,CompletedQty,PendingReason")] SamCutAssignmentMaster samCutAssignmentMaster)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(samCutAssignmentMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatternMasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName", samCutAssignmentMaster.PatternMasterID);
            ViewBag.SampCutreqID = new SelectList(db.SampCutReqMasters, "SampCutreqID", "ReqNum", samCutAssignmentMaster.SampCutreqID);
            return View(samCutAssignmentMaster);
        }

        // GET: SamCutAssignmentMasters/Delete/5
        public ActionResult Delete(decimal id)
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
            return View(samCutAssignmentMaster);
        }

        // POST: SamCutAssignmentMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SamCutAssignmentMaster samCutAssignmentMaster = db.SamCutAssignmentMasters.Find(id);
            db.SamCutAssignmentMasters.Remove(samCutAssignmentMaster);
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



        public ActionResult AssignRequest()
        {
        //    var model = new List<RequestSignModel>();
            Models.AssignRequestModel sgreqmodel = new Models.AssignRequestModel();
            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
                    where smpasg.ReceivedBy !=null 
                    select new { sampmstr.ReqNum, smpasg.CutAssignID };
            sgreqmodel.Reqnumlist = new List<Models.RequestSignModel>(); ;







            var sampCutReqMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster. IsReceived == "Y" && s.PatternMasterID==null ).Include(s => s.SampCutReqMaster. BuyerMaster).Include(s => s.SampCutReqMaster.PatternStyle).Include(s => s.SampCutReqMaster.PatterRefMaster).Include(s => s.SampCutReqMaster.SampleType);


            var sampCutReqMasterssort = sampCutReqMasters.ToList().OrderByDescending(a => a.ReceivedDate
            );
            ViewBag.sampCutReqMasterssort = sampCutReqMasterssort.ToList();











            ViewBag.CutAssignID = new SelectList(q, "CutAssignID", "ReqNum");
            ViewBag.patternmasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName");
            return View(sgreqmodel);
        }


        public ActionResult AssignRequestNew(int cutId, int pattermasterid,int priority )
        {


            var q = from sampass in db.SamCutAssignmentMasters
                    where sampass.CutAssignID == cutId
                    select sampass;

            foreach (var element in q)
            {
                element.PatternMasterID = pattermasterid;
                element.AssignedDate = DateTime.Now;
                
            }

            db.SaveChanges();


         return   RedirectToAction("AssignRequest");
           
        }
        [HttpGet]
        public JsonResult PopulateDetails(int Id=0)
        {

            JsonResult jsd = Json(GetDetailsofaspecificTicket(Id),JsonRequestBehavior.AllowGet);

              return jsd;

           // return Json(new { status = "Success", message = "Success" }, JsonRequestBehavior.AllowGet);
        }
  

        public List<RequestSignModel> GetDetailsofaspecificTicket( int id)
        {
            var model = new List<RequestSignModel>();
            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
                    join patternrefmaster in db.PatterRefMasters on sampmstr.PatternRefID equals patternrefmaster.PatternRefID
                    where  smpasg.CutAssignID==id
                    select new { sampmstr.ReqNum,
                        sampmstr.BuyerMaster.BuyerName,
                        sampmstr.PatterRefMaster.PatterRefNum,
                        sampmstr.PatternStyle.StyleName, 
                        sampmstr.Fabric, sampmstr.SampleType.SampleType1,
                        sampmstr.SampleRequiredDate, smpasg.PatternMaster.PaternMasterName,
                        sampmstr.AddedBy, smpasg.SignedBYMaster, smpasg.SignedDate,smpasg.PatternReqDate };
                  //var newdata = q.ToList();

            foreach (var element in q)
            {
                RequestSignModel rsgmp = new Models.RequestSignModel();
                rsgmp.ReqNum = element.ReqNum;


                rsgmp.BuyerName = element.BuyerName;

                rsgmp.PatterRefNum = element.PatterRefNum;

                rsgmp.StyleName = element.StyleName;

             


                rsgmp.Fabric = element.Fabric;

                rsgmp.SampleType = element.SampleType1;

                rsgmp.SampleRequiredDate = element.SampleRequiredDate.ToString();

                rsgmp.PaternMasterName = element.PaternMasterName;

                rsgmp.PatternReqDateString =  element.PatternReqDate.ToString();

                rsgmp.SignedBYMaster = element.SignedBYMaster;
                rsgmp.AddedBy = element.AddedBy;

                model.Add(rsgmp);
            }




            //  ViewBag.SampCutreqID = new SelectList(q, "CutAssignID", "ReqNum");
            return model;
        }

        public ActionResult ShowPendingPatternCompleted()
        {


            var samCutAssignmentMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster.MarkCompleted == null && s.PatternCompletedDate==null).Include(s => s.PatternMaster).Include(s => s.SampCutReqMaster);

            var samCutAssignmentMasterssort = samCutAssignmentMasters.ToList().OrderByDescending(a => a.ReceivedDate);

            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
                    where smpasg.PatternCompletedDate == null
                    select new { sampmstr.ReqNum, smpasg.CutAssignID };


            ViewBag.CutAssignID = new SelectList(q, "CutAssignID", "ReqNum");
            return View(samCutAssignmentMasterssort.ToList());

        }


        public ActionResult PatternCompleted(int CutAssignID, String Fromdate)
        {


            var q = from sampass in db.SamCutAssignmentMasters
                    where sampass.CutAssignID == CutAssignID
                    select sampass;

            foreach (var element in q)
            {
                element.PatternCompletedDate = DateTime.Parse (Fromdate);
               

            }

            db.SaveChanges();


            return RedirectToAction("ShowPendingPatternCompleted");

        }

    }
}
