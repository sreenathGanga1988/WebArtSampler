using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebArtSampler.Areas.Planning.Models;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Planning.Controllers
{
    public class CuttingPriorityController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: Planning/CuttingPriority
        public ActionResult Index()
        {
            Models.CutplanPriority cmmodel = new Models.CutplanPriority();

            List<Models.RequestCuttingPriority> model = GetDetailsofaspecificTicket();

            cmmodel.RequestCuttingPrioritylist = model;
            return View(cmmodel);
        }

        // GET: Planning/CuttingPriority/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Planning/CuttingPriority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Planning/CuttingPriority/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebArtSampler.Areas.Planning.Models.CutplanPriority cmmodel)
        {
            try
            {

               
                    foreach (RequestCuttingPriority item in cmmodel.RequestCuttingPrioritylist.ToList())
                    {


                        if (item.isSelected == true)
                        {


                        SamCutPlanPriority cutpriority = new WebArtSampler.Models.SamCutPlanPriority();
                        cutpriority.CutAssignID = item.CuttingAssignId;
                        cutpriority.PlanDate = cmmodel.Fromdate;
                        cutpriority.Priority = item.Priority;
                        cutpriority.ToCutQty = item.NewQty;

                        db.SamCutPlanPriorities.Add(cutpriority);
                   


                    }

                    }

                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Planning/CuttingPriority/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Planning/CuttingPriority/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Planning/CuttingPriority/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Planning/CuttingPriority/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        public List<Models.RequestCuttingPriority> GetDetailsofaspecificTicket()
        {
            List<Models.RequestCuttingPriority> model = new List<Models.RequestCuttingPriority>();

            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
                    join patternrefmaster in db.PatterRefMasters on sampmstr.PatternRefID equals patternrefmaster.PatternRefID
                   where sampmstr.IsReceived=="Y" && sampmstr.MarkCompleted == null
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
                        sampmstr.Qty,
                        smpasg.Remark,
                        smpasg.CompletedQty,
                      cuttingremark=  sampmstr.Remark
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

                rsgmp.SampleRequiredDate =  element.SampleRequiredDate;

                rsgmp.PaternMasterName = element.PaternMasterName;

                rsgmp.PatternReqDate= element.PatternReqDate;

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
