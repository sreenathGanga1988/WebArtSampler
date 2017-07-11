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
namespace WebArtSampler.Areas.Cutting
{
    public class DeliveryController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: Cutting/Delivery
        public ActionResult Index()
        {
          return  RedirectToAction("ShowPendingDelivery");
        }

        public ActionResult ShowPendingDelivery()
        {
            var samCutAssignmentMasters = db.SamCutAssignmentMasters.Where(s => s.SampCutReqMaster.MarkCompleted == null && s.PatternCompletedDate!=null).Include(s => s.PatternMaster).Include(s => s.SampCutReqMaster);

            var samCutAssignmentMasterssort = samCutAssignmentMasters.ToList().OrderByDescending(a => a.ReceivedDate).ToList();
            foreach (var element in samCutAssignmentMasterssort.ToList())
            {






          
                int Size1SewQty = element.SampCutReqMaster.Size1SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1SewQty.ToString());
                int Size2SewQty = element.SampCutReqMaster.Size2SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2SewQty.ToString());
                int Size3SewQty = element.SampCutReqMaster.Size3SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3SewQty.ToString());
                int Size4SewQty = element.SampCutReqMaster.Size4SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4SewQty.ToString());
                int Size5SewQty = element.SampCutReqMaster.Size5SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5SewQty.ToString());
                int Size6SewQty = element.SampCutReqMaster.Size6SewQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6SewQty.ToString());


                int Size1DeliveredQty = element.SampCutReqMaster.Size1DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size1DeliveredQty.ToString());
                int Size2DeliveredQty = element.SampCutReqMaster.Size2DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size2DeliveredQty.ToString());
                int Size3DeliveredQty = element.SampCutReqMaster.Size3DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size3DeliveredQty.ToString());
                int Size4DeliveredQty = element.SampCutReqMaster.Size4DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size4DeliveredQty.ToString());
                int Size5DeliveredQty = element.SampCutReqMaster.Size5DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size5DeliveredQty.ToString());
                int Size6DeliveredQty = element.SampCutReqMaster.Size6DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.SampCutReqMaster.Size6DeliveredQty.ToString());



                int alreadydone = Size1DeliveredQty + Size2DeliveredQty + Size3DeliveredQty + Size4DeliveredQty + Size5DeliveredQty + Size6DeliveredQty;
                int totaltodo = Size1SewQty + Size2SewQty + Size3SewQty + Size4SewQty + Size5SewQty + Size6SewQty;

                if (alreadydone == totaltodo)
                {
                    samCutAssignmentMasterssort.Remove(element);
                }
            }
                return View(samCutAssignmentMasterssort.ToList());
        }




        public ActionResult UpdateDelivery(int id)
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
        public ActionResult UpdateDelivery(SewingViewModel svmdl)
        {

            var q = from samre in db.SampCutReqMasters
                    where samre.SampCutreqID == svmdl.samcutAssignmentmaster.SampCutReqMaster.SampCutreqID
                    select samre;

            foreach (var element in q)
            {
                element.Size1DeliveredQty = int.Parse(element.Size1DeliveredQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
                element.Size2DeliveredQty = int.Parse(element.Size2DeliveredQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
                element.Size3DeliveredQty = int.Parse(element.Size3DeliveredQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty3CutNew.ToString());
                element.Size4DeliveredQty = int.Parse(element.Size4DeliveredQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty4CutNew.ToString());
                element.Size5DeliveredQty = int.Parse(element.Size5DeliveredQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty5CutNew.ToString());
                element.Size6DeliveredQty = int.Parse(element.Size6DeliveredQty.ToString()) + int.Parse(svmdl.samcutAssignmentmaster.SizeQty6CutNew.ToString());


            }

            SamDailyDeliveryStatu smpsew = new WebArtSampler. Models.SamDailyDeliveryStatu();
            smpsew.SampCutreqID = svmdl.samcutAssignmentmaster.SampCutReqMaster.SampCutreqID;
            smpsew.Size1DeliveryQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty1CutNew.ToString());

            smpsew.Size1DeliveryQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty2CutNew.ToString());
            smpsew.Size1DeliveryQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty3CutNew.ToString());
            smpsew.Size1DeliveryQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty4CutNew.ToString());
            smpsew.Size1DeliveryQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty5CutNew.ToString());
            smpsew.Size1DeliveryQty = int.Parse(svmdl.samcutAssignmentmaster.SizeQty6CutNew.ToString());

            smpsew.DateofAction = DateTime.Parse(svmdl.DateofAction.ToString());

            db.SamDailyDeliveryStatus.Add(smpsew);

            db.SaveChanges();
            return RedirectToAction("ShowPendingDelivery");
        }



    }
}