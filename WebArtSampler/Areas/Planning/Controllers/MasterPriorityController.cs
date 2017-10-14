using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Planning.Controllers
{
    public class MasterPriorityController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: Planning/MasterPriority
        public ActionResult ShowPendingPattern()
        {
            Models.MasterDailyPriority cmmodel = new Models.MasterDailyPriority();

            List<Models.RequestCuttingPriority> model = GetDetailsofaspecificTicket();

            cmmodel.RequestCuttingPrioritylist = model;
            return View(cmmodel);
        }

        public List<Models.RequestCuttingPriority> GetDetailsofaspecificTicket()
        {
            List<Models.RequestCuttingPriority> model = new List<Models.RequestCuttingPriority>();

            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
                    join patternrefmaster in db.PatterRefMasters on sampmstr.PatternRefID equals patternrefmaster.PatternRefID
                    where sampmstr.IsReceived == "Y" && smpasg.PatternCompletedDate == null
                    select new
                    {
                        smpasg.CutAssignID,
                        sampmstr.ReqNum,
                        sampmstr.BuyerMaster.BuyerName,
                        sampmstr.PatterRefMaster.PatterRefNum,
                        sampmstr.PatternStyle.StyleName,
                        sampmstr.Fabric,
                        sampmstr.SampleType.SampleType1,
                        sampmstr.SampleRequiredDate,
                        smpasg.PatternReqDate,
                        smpasg.PatternMaster.PaternMasterName,
                        sampmstr.AddedBy,
                        smpasg.SignedBYMaster,
                        smpasg.SignedDate,
                        smpasg.ReceivedDate,
                        sampmstr.AddedDate,
                        sampmstr.Qty,
                        smpasg.Remark,
                        smpasg.CompletedQty,
                        cuttingremark = sampmstr.Remark
                    };
            //var newdata = q.ToList();

            foreach (var element in q)
            {
                Models.RequestCuttingPriority rsgmp = new Models.RequestCuttingPriority();
                rsgmp.ReqNum = element.ReqNum;
                rsgmp.Fabric = element.Fabric;
                rsgmp.CuttingAssignId = element.CutAssignID;
                rsgmp.BuyerName = element.BuyerName;

                rsgmp.PatterRefNum = element.PatterRefNum;

                rsgmp.StyleName = element.StyleName;






                rsgmp.SampleType = element.SampleType1;

                rsgmp.SampleRequiredDate = element.SampleRequiredDate;

                rsgmp.PaternMasterName = element.PaternMasterName;

                rsgmp.PatternReqDate = element.PatternReqDate;

                rsgmp.SignedBYMaster = element.SignedBYMaster;
                rsgmp.AddedBy = element.AddedBy;
                rsgmp.CompletedQty = element.CompletedQty;
                rsgmp.Remark = element.Remark;
                rsgmp.Qty = element.Qty;
                rsgmp.isSelected = false;
                rsgmp.Expr1 = element.cuttingremark;
                model.Add(rsgmp);
            }




            //  ViewBag.SampCutreqID = new SelectList(q, "CutAssignID", "ReqNum");
            return model;
        }



    }
}