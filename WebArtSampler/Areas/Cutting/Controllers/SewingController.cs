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
    public class SewingController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: Cutting/Sewing
        public ActionResult ShowPendingToSew()
        {
            var samCutAssignmentMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster.MarkCompleted == null && s.PatternCompletedDate != null).Include(s => s.PatternMaster).Include(s => s.SampCutReqMaster);


            var samCutAssignmentMasterssort = samCutAssignmentMasters.ToList().OrderByDescending(a => a.ReceivedDate).ToList();

            foreach (var element in samCutAssignmentMasterssort.ToList())
            {




               

                int Size1CutQty = element.SampCutReqMaster.Size1CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1CutQty.ToString());
                int Size2CutQty = element.SampCutReqMaster.Size2CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2CutQty.ToString());
                int Size3CutQty = element.SampCutReqMaster.Size3CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3CutQty.ToString());
                int Size4CutQty = element.SampCutReqMaster.Size4CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4CutQty.ToString());
                int Size5CutQty = element.SampCutReqMaster.Size5CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5CutQty.ToString());
                int Size6CutQty = element.SampCutReqMaster.Size6CutQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6CutQty.ToString());


                int Size1SewQty = element.SampCutReqMaster.Size1SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1SewQty.ToString());
                int Size2SewQty = element.SampCutReqMaster.Size2SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2SewQty.ToString());
                int Size3SewQty = element.SampCutReqMaster.Size3SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3SewQty.ToString());
                int Size4SewQty = element.SampCutReqMaster.Size4SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4SewQty.ToString());
                int Size5SewQty = element.SampCutReqMaster.Size5SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5SewQty.ToString());
                int Size6SewQty = element.SampCutReqMaster.Size6SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6SewQty.ToString());



              
                int totaltodo = Size1CutQty + Size2CutQty + Size3CutQty + Size4CutQty + Size5CutQty + Size6CutQty;
                int alreadydone = Size1SewQty + Size2SewQty + Size3SewQty + Size4SewQty + Size5SewQty + Size6SewQty;


                element.TotalCut = totaltodo;
                element.TotalSew = alreadydone;
                element.BalanceToSew = totaltodo - alreadydone;

                if (alreadydone == totaltodo)
                {
                    samCutAssignmentMasterssort.Remove(element);
                }
            }
                return View(samCutAssignmentMasterssort.ToList());
        }


        public ActionResult UpdateSew(int id)
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

            SewingViewModel svmdl = new Models.SewingViewModel();
            svmdl.DateofAction = DateTime.Now;
            svmdl.samcutAssignmentmaster = samCutAssignmentMaster;






            return View(svmdl);



           


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSew(SewingViewModel svmdl)
        {

            var q = from samre in db.SampCutReqMasters
                    where samre.SampCutreqID == svmdl.samcutAssignmentmaster.SampCutReqMaster.SampCutreqID
                    select samre;

            foreach (var element in q)
            {
                element.Size1SewQty = int.Parse(element.Size1SewQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty1CutNew.ToString());
                element.Size2SewQty = int.Parse(element.Size2SewQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
                element.Size3SewQty = int.Parse(element.Size3SewQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty3CutNew.ToString());
                element.Size4SewQty = int.Parse(element.Size4SewQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty4CutNew.ToString());
                element.Size5SewQty = int.Parse(element.Size5SewQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty5CutNew.ToString());
                element.Size6SewQty = int.Parse(element.Size6SewQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty6CutNew.ToString());


            }

            SamDailySewStatu smpsew = new WebArtSampler. Models.SamDailySewStatu();
            smpsew.SampCutreqID = svmdl.samcutAssignmentmaster.SampCutReqMaster.SampCutreqID;
            smpsew.Size1SewtQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty1CutNew.ToString());

            smpsew.Size2SewQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
            smpsew.Size3SewQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty3CutNew.ToString());
            smpsew.Size4SewQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty4CutNew.ToString());
            smpsew.Size5SewQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty5CutNew.ToString());
            smpsew.Size6SewQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty6CutNew.ToString());

            smpsew.DateofAction = DateTime.Parse(svmdl.DateofAction.ToString());

            db.SamDailySewStatus.Add(smpsew);

            db.SaveChanges();
            return RedirectToAction("ShowPendingToSew");
        }


    }


}