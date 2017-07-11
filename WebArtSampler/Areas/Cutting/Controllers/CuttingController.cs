using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebArtSampler.Areas.Cutting.Models;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Cutting.Controllers
{
    public class CuttingController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: Cutting/Cutting
        public ActionResult Index()
        {
            var samCutAssignmentMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster.MarkCompleted == null && s.PatternCompletedDate!=null).Include(s => s.PatternMaster).Include(s => s.SampCutReqMaster);


            var samCutAssignmentMasterssort = samCutAssignmentMasters.ToList().OrderByDescending(a => a.ReceivedDate).ToList();

            foreach(var element in samCutAssignmentMasterssort.ToList())
            {
                
                

                int Qty1 =element.SampCutReqMaster.Qty1.Equals(null) ? 0: Convert.ToInt32(element.SampCutReqMaster.Qty1.ToString ());
                int Qty2 = element.SampCutReqMaster.Qty2.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Qty2.ToString());
                int Qty3 = element.SampCutReqMaster.Qty3.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Qty3.ToString());
                int Qty4= element.SampCutReqMaster.Qty4.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Qty4.ToString());
                int Qty5 = element.SampCutReqMaster.Qty5.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Qty5.ToString());
                int Qty6 = element.SampCutReqMaster.Qty6.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Qty6.ToString());


                int Size1CutQty = element.SampCutReqMaster.Size1CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1CutQty.ToString());
                int Size2CutQty = element.SampCutReqMaster.Size2CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2CutQty.ToString());
                int Size3CutQty = element.SampCutReqMaster.Size3CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3CutQty.ToString());
                int Size4CutQty = element.SampCutReqMaster.Size4CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4CutQty.ToString());
                int Size5CutQty = element.SampCutReqMaster.Size5CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5CutQty.ToString());
                int Size6CutQty = element.SampCutReqMaster.Size6CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6CutQty.ToString());



                int totaltodo = Qty1 + Qty2 + Qty3 + Qty4 + Qty5 + Qty6;
                int alreadydone = Size1CutQty + Size2CutQty + Size3CutQty + Size4CutQty + Size5CutQty + Size6CutQty;
                element.TotalCut = alreadydone;
                element.Balancetocut = totaltodo-alreadydone;

                if (alreadydone== totaltodo)
                {
                    samCutAssignmentMasterssort.Remove(element);
                }
            }

            return View(samCutAssignmentMasterssort.ToList());
        }

        public ActionResult UpdateCut(int id )
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

            CuttingViewModel cvmdl = new Models.CuttingViewModel();
            cvmdl.DateofAction = DateTime.Now;
            cvmdl.samcutAssignmentmaster = samCutAssignmentMaster;

            return View(cvmdl);

           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCut(CuttingViewModel cvmdl)
        {

            var q = from samre in db.SampCutReqMasters
                    where samre.SampCutreqID == cvmdl.samcutAssignmentmaster.SampCutReqMaster.SampCutreqID
                    select samre;

            foreach(var element in q)
            {
                element.Size1CutQty = int.Parse(element.Size1CutQty.ToString()) + int.Parse(cvmdl.samcutAssignmentmaster.SizeQty1CutNew.ToString());
                element.Size2CutQty = int.Parse(element.Size2CutQty.ToString()) + int.Parse(cvmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
                element.Size3CutQty = int.Parse(element.Size3CutQty.ToString()) + int.Parse(cvmdl.samcutAssignmentmaster.SizeQty3CutNew.ToString());
                element.Size4CutQty = int.Parse(element.Size4CutQty.ToString()) + int.Parse(cvmdl.samcutAssignmentmaster.SizeQty4CutNew.ToString());
                element.Size5CutQty = int.Parse(element.Size5CutQty.ToString()) + int.Parse(cvmdl.samcutAssignmentmaster.SizeQty5CutNew.ToString());
                element.Size6CutQty = int.Parse(element.Size6CutQty.ToString()) + int.Parse(cvmdl.samcutAssignmentmaster.SizeQty6CutNew.ToString());

               
            }
            
            SamDailyCutStatu smpcut = new WebArtSampler.Models.SamDailyCutStatu();
            smpcut.SampCutreqID = cvmdl.samcutAssignmentmaster.SampCutReqMaster.SampCutreqID;
            smpcut.Size1CutQty = int.Parse(cvmdl.samcutAssignmentmaster.SizeQty1CutNew.ToString());

            smpcut.Size2CutQty = int.Parse(cvmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
            smpcut.Size3CutQty =  int.Parse(cvmdl.samcutAssignmentmaster.SizeQty3CutNew.ToString());
            smpcut.Size4CutQty =  int.Parse(cvmdl.samcutAssignmentmaster.SizeQty4CutNew.ToString());
            smpcut.Size5CutQty =  int.Parse(cvmdl.samcutAssignmentmaster.SizeQty5CutNew.ToString());
            smpcut.Size6CutQty = int.Parse(cvmdl.samcutAssignmentmaster.SizeQty6CutNew.ToString());

            smpcut.DateofAction = DateTime.Parse(cvmdl.DateofAction.ToString());
            
            db.SamDailyCutStatus.Add(smpcut);

            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult GetCuttingofADate(DailyCuttingViewModel vmdl=null)
        {
            if (vmdl.SamDailyCutStatu == null)
            {
                vmdl = new Models.DailyCuttingViewModel();
                vmdl = getcutDataofDate(DateTime.Now);
            }
            else
            {

            }
            return View(vmdl);
        }


      
        public DailyCuttingViewModel getcutDataofDate(DateTime date)
        {
            DailyCuttingViewModel vmdl = new Models.DailyCuttingViewModel();

            vmdl.DateofAction = date;

            var sampCutReqMasters = (db.SamDailyCutStatus.Where(s => s.DateofAction == date).Include(s => s.SampCutReqMaster)).ToList();


            vmdl.SamDailyCutStatu = sampCutReqMasters;

            return vmdl;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCuttingofADate(DateTime date)
        {
            DailyCuttingViewModel vmdl = new Models.DailyCuttingViewModel();

            vmdl.DateofAction = date;

            var sampCutReqMasters = (db.SamDailyCutStatus.Where(s => s.DateofAction == date).Include(s => s.SampCutReqMaster)).ToList();


            vmdl.SamDailyCutStatu = sampCutReqMasters;

            return RedirectToAction("GetCuttingofADate", "Cutting", vmdl);
        }

       

       
    }
}