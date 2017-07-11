using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebArtSampler.Models;

namespace WebArtSampler.Controllers
{
    public class MastersActionController : Controller
    {
        private ArtEntities db = new ArtEntities();
        // GET: MastersAction
        public ActionResult Index()
        {
            var model = new List<RequestSignModel>();
            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
join patternrefmaster in db.PatterRefMasters on sampmstr.PatternRefID equals patternrefmaster.PatternRefID
                    where sampmstr.MarkCompleted== null                
select new { sampmstr.SampCutreqID, sampmstr.ReqNum, sampmstr.BuyerMaster.BuyerName , sampmstr.PatterRefMaster.PatterRefNum,sampmstr.PatternStyle.StyleName,sampmstr.StyleDescription,sampmstr.Fabric,sampmstr.SampleType.SampleType1,sampmstr.SampleRequiredDate,smpasg.PatternMaster.PaternMasterName,sampmstr.AddedBy,smpasg.SignedBYMaster,smpasg.SignedDate };


            //var newdata = q.ToList();

            foreach(var element in q)
            {
                RequestSignModel rsgmp = new Models.RequestSignModel();
                rsgmp.ReqNum = element.ReqNum;

                rsgmp.cutreqid =int.Parse ( element.SampCutreqID.ToString ());
               rsgmp.BuyerName= element.BuyerName;

                rsgmp.PatterRefNum =element.PatterRefNum;

                rsgmp.StyleName = element.StyleName;

                rsgmp.StyleDescription = element.StyleDescription;

                rsgmp.Fabric = element.Fabric;

                rsgmp.SampleType = element.SampleType1;

                rsgmp.SampleRequiredDate = element.SampleRequiredDate.ToString ();

                rsgmp.PaternMasterName = element.PaternMasterName;

                rsgmp.PaternMasterName = element.PaternMasterName;

                rsgmp.SignedBYMaster = element.SignedBYMaster;
                rsgmp.AddedBy = element.AddedBy;

                model.Add(rsgmp);
            }

         


            //  ViewBag.SampCutreqID = new SelectList(q, "CutAssignID", "ReqNum");
            return View(model);
        }


        public ActionResult SignRequest()
        {
            var model = new List<RequestSignModel>();
            Models.RequestSignModel sgreqmodel = new Models.RequestSignModel();
            var q = from sampmstr in db.SampCutReqMasters
                    join
smpasg in db.SamCutAssignmentMasters on sampmstr.SampCutreqID equals smpasg.SampCutreqID
                    where smpasg.SignedBYMaster == false
                    select new { sampmstr.ReqNum, smpasg.CutAssignID };

            ViewBag.CutAssignID = new SelectList(q, "CutAssignID", "ReqNum");

            return View();
        }

        


        public ActionResult EditSignRequest(int  Id)
        {
            

            var q = from sampass in db.SamCutAssignmentMasters
                    where sampass.CutAssignID == Id
                    select sampass;

            foreach (var element in q)
            {
                element.SignedBYMaster = true;
                element.SignedDate = DateTime.Now;
            }

            db.SaveChanges();

            return View();
        }


        public ActionResult ShowHistory()
        {


            return View();
        }
    }
}