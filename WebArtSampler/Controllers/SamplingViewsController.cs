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
    public class SamplingViewsController : Controller
    {
        private ArtEntities db = new ArtEntities();



        public ActionResult Index(int Id = 0, string Fromdate = null, string todate = null, int BuyerID = 0)
        {
            ViewBag.TotalRecords = 0;
            ViewBag.Signedrecord = 0;
            ViewBag.Completedcode = 0;
            ViewBag.patternmasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName");


            ViewBag.BuyerID = new SelectList(db.BuyerMasters.Where(o => o.IsActive == "Y"), "BuyerID", "BuyerName");
            System.Web.HttpContext.Current.Session["Reportype"] = "All";



            if (Id == 0 && Fromdate != null && todate != null && Fromdate.Trim() != "" && todate.Trim() != "" && BuyerID == 0)
            {
                System.Web.HttpContext.Current.Session["Reportype"] = "AllWithinPeriod";




                System.Web.HttpContext.Current.Session["fromdate"] = Fromdate;
                System.Web.HttpContext.Current.Session["todate"] = todate;
            }
            else if (Id != 0 && Fromdate == null && todate == null && Fromdate.Trim() == "" && todate.Trim() == "" && BuyerID == 0)
            {


                var mastername = db.PatternMasters.Where(u => u.PatternMasterID == Id).Select(u => u.PaternMasterName).FirstOrDefault();
                System.Web.HttpContext.Current.Session["mastername"] = mastername.ToString();
                System.Web.HttpContext.Current.Session["Reportype"] = "Allofmaster";
            }
            else if (Id != 0 && Fromdate != null && todate != null && Fromdate.Trim() != "" && todate.Trim() != "" && BuyerID == 0)
            {
                var mastername = db.PatternMasters.Where(u => u.PatternMasterID == Id).Select(u => u.PaternMasterName).FirstOrDefault();

                System.Web.HttpContext.Current.Session["mastername"] = mastername.ToString();
                System.Web.HttpContext.Current.Session["Reportype"] = "Permaster";
                System.Web.HttpContext.Current.Session["fromdate"] = Fromdate;
                System.Web.HttpContext.Current.Session["todate"] = todate;
                System.Web.HttpContext.Current.Session["empid"] = Id;
            }

            else if (Id == 0 && Fromdate != null && todate != null && Fromdate.Trim() != "" && todate.Trim() != "" && BuyerID != 0)
            {
                var BuyerName = db.BuyerMasters.Where(u => u.BuyerID == BuyerID).Select(u => u.BuyerName).FirstOrDefault();


                System.Web.HttpContext.Current.Session["Reportype"] = "BuyerWithinPeriod";
                System.Web.HttpContext.Current.Session["fromdate"] = Fromdate;
                System.Web.HttpContext.Current.Session["todate"] = todate;
                System.Web.HttpContext.Current.Session["empid"] = Id;
                System.Web.HttpContext.Current.Session["BuyerID"] = BuyerID;
                System.Web.HttpContext.Current.Session["BuyerName"] = BuyerName;

            }

            else
            {

            }

            List<SamplingView> varlist = db.SamplingViews.ToList();
            if (Id == 0 && Fromdate == null && todate == null)
            {
                if (BuyerID != 0)
                {
                    varlist = varlist.Where(o => o.BuyerID == Id).ToList();
                    return View(varlist);
                }
                else
                {
                    return View(db.SamplingViews.ToList());
                }


            }
            else
            {

                if (Id != 0)
                {
                    varlist = varlist.Where(o => o.PatternMasterID == Id).ToList();
                }


                if (Fromdate != null && todate != null && Fromdate != "" && todate != "")
                {
                    DateTime fromdateof = DateTime.Parse(Fromdate);
                    DateTime todatetodate = DateTime.Parse(todate);
                    varlist = varlist.Where(o => o.AddedDate >= fromdateof && o.AddedDate <= todatetodate).ToList();
                }
                if (BuyerID != 0)
                {
                    varlist = varlist.Where(o => o.BuyerID == BuyerID).ToList();
                }
                else
                {

                }
            }

            try
            {
                ViewBag.TotalRecords = varlist.Count();
            }
            catch (Exception)
            {


            }
            try
            {
                var signedlist = varlist.Where(p => p.SignedBYMaster = true);
                ViewBag.Signedrecord = signedlist.Count();
            }
            catch (Exception)
            {


            }
            try
            {
                var completedlist = varlist.Where(p => p.MarkedCompletedBY != null);
                ViewBag.Completedcode = completedlist.Count();
            }
            catch (Exception)
            {


            }


            return View(varlist);

        }




        public ActionResult SearchByFabric(String Id = "", string Fromdate = null, string todate = null)
        {

            ViewBag.TotalRecords = 0;
            ViewBag.Signedrecord = 0;
            ViewBag.Completedcode = 0;
            //   ViewBag.patternmasterID = new SelectList(db.SampCutReqMasters, "Fabric", "Fabric");


            var items = db.SampCutReqMasters.Where(m => m.SampCutreqID != 0)
                    .OrderBy(m => m.Fabric)
                    .Select(i => i.Fabric)
                    .Distinct();
            ViewBag.patternmasterID = new SelectList(items);

            System.Web.HttpContext.Current.Session["Reportype"] = "All";

            List<SamplingView> varlist = db.SamplingViews.ToList();

            if (Id == "" && Fromdate == null && todate == null)
            {


                return View(varlist);




            }
            else
            {

                if (Id != "")
                {
                    varlist = varlist.Where(o => o.Fabric == Id).ToList();




                }

                if (Fromdate != null && todate != null && Fromdate != "" && todate != "")
                {
                    DateTime fromdateof = DateTime.Parse(Fromdate);
                    DateTime todatetodate = DateTime.Parse(todate);
                    varlist = varlist.Where(o => o.AddedDate >= fromdateof && o.AddedDate <= todatetodate).ToList();
                    System.Web.HttpContext.Current.Session["fromdate"] = Fromdate;
                    System.Web.HttpContext.Current.Session["todate"] = todate;
                }



                if (Id != "" && Fromdate != null && todate != null && Fromdate != "" && todate != "")
                {
                    System.Web.HttpContext.Current.Session["Reportype"] = "Fabric Within Period";
                    System.Web.HttpContext.Current.Session["fromdate"] = Fromdate;
                    System.Web.HttpContext.Current.Session["todate"] = todate;

                    System.Web.HttpContext.Current.Session["fabric"] = Id;
                }






                try
                {
                    ViewBag.TotalRecords = varlist.Count();
                }
                catch (Exception)
                {


                }
                try
                {
                    var signedlist = varlist.Where(p => p.SignedBYMaster = true);
                    ViewBag.Signedrecord = signedlist.Count();
                }
                catch (Exception)
                {


                }
                try
                {
                    var completedlist = varlist.Where(p => p.MarkedCompletedBY != null);
                    ViewBag.Completedcode = completedlist.Count();
                }
                catch (Exception)
                {


                }


                return View(varlist);
            }

        }



        public ActionResult getPendingOfday()
        {
            System.Web.HttpContext.Current.Session["Reportype"] = "Pending";
            Session["subtype"] = "";
            return RedirectToAction("LoadReport");

        }

        public ActionResult getPendingOfdaySample()
        {
            System.Web.HttpContext.Current.Session["Reportype"] = "Pending";
            System.Web.HttpContext.Current.Session["subtype"] = "Sample";
            return RedirectToAction("LoadReport");

        }











        public void getdataofBuyer()
        {

        }





        public ActionResult SearchofMaster(int Id, string Fromdate, string todate)
        {

            ViewBag.patternmasterID = new SelectList(db.PatternMasters, "PatternMasterID", "PaternMasterName");

            return View(db.SamplingViews.Where(o => o.PatternMasterID == Id && o.CompletedDate >= DateTime.Parse(Fromdate) && o.CompletedDate <= DateTime.Parse(todate)).ToList());
        }



        public ActionResult LoadReport()
        {





            //  return Redirect("~/Views/Reports/Reportviewer.aspx");
            return View();
        }



        // GET: SamplingViews/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamplingView samplingView = db.SamplingViews.Find(id);
            if (samplingView == null)
            {
                return HttpNotFound();
            }
            return View(samplingView);
        }

        // GET: SamplingViews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SamplingViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReqNum,Fabric,StyleDescription,BuyerName,PatterRefNum,StyleName,SampleRequiredDate,AddedDate,AddedBy,ReceivedDate,ReceivedBy,SignedBYMaster,SignedDate,CompletedDate,Remark,MarkedCompletedBY,PaternMasterName")] SamplingView samplingView)
        {
            if (ModelState.IsValid)
            {
                db.SamplingViews.Add(samplingView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(samplingView);
        }

        // GET: SamplingViews/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamplingView samplingView = db.SamplingViews.Find(id);
            if (samplingView == null)
            {
                return HttpNotFound();
            }
            return View(samplingView);
        }

        // POST: SamplingViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReqNum,Fabric,StyleDescription,BuyerName,PatterRefNum,StyleName,SampleRequiredDate,AddedDate,AddedBy,ReceivedDate,ReceivedBy,SignedBYMaster,SignedDate,CompletedDate,Remark,MarkedCompletedBY,PaternMasterName")] SamplingView samplingView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samplingView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(samplingView);
        }

        // GET: SamplingViews/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamplingView samplingView = db.SamplingViews.Find(id);
            if (samplingView == null)
            {
                return HttpNotFound();
            }
            return View(samplingView);
        }

        // POST: SamplingViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            SamplingView samplingView = db.SamplingViews.Find(id);
            db.SamplingViews.Remove(samplingView);
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








        public ActionResult CutReqView(int id=0)
        {




            return View(getdata(id));
        }




        public CutreQViewModel getdata(int cutreQid)
        {
            var q = (from cutassign in db.SamCutAssignmentMasters
                    where cutassign.SampCutreqID == cutreQid
                    select new CutreQViewModel
                    {

                        ReqNum = cutassign.SampCutReqMaster.ReqNum,

                        SampleRequiredDate = cutassign.SampCutReqMaster.SampleRequiredDate,
                        AddedDate = cutassign.SampCutReqMaster.AddedDate,
                        Qty = cutassign.SampCutReqMaster.Qty,
                        MarkCompleted = cutassign.SampCutReqMaster.MarkCompleted,
                        MarkedCompletedDate = cutassign.SampCutReqMaster.MarkedCompletedDate,
                        Fabric = cutassign.SampCutReqMaster.Fabric,
                        StyleDescription = cutassign.SampCutReqMaster.StyleDescription,
                        AddedBy = cutassign.SampCutReqMaster.AddedBy,
                        SizeDetail = cutassign.SampCutReqMaster.SizeDetail,
                        MarkCompletedBy = cutassign.SampCutReqMaster.MarkCompletedBy,
                        Remark = cutassign.SampCutReqMaster.Remark,
                        IsTeckPack = cutassign.SampCutReqMaster.IsTeckPack,
                        IsReceived = cutassign.SampCutReqMaster.IsReceived,
                        Size1 = cutassign.SampCutReqMaster.Size1,
                        Size2 = cutassign.SampCutReqMaster.Size2,
                        Size3 = cutassign.SampCutReqMaster.Size3,
                        Size4 = cutassign.SampCutReqMaster.Size4,
                        Size5 = cutassign.SampCutReqMaster.Size5,
                        Size6 = cutassign.SampCutReqMaster.Size6,
                        Qty1 = cutassign.SampCutReqMaster.Qty1,
                        Qty2 = cutassign.SampCutReqMaster.Qty2,
                        Qty3 = cutassign.SampCutReqMaster.Qty3,
                        Qty4 = cutassign.SampCutReqMaster.Qty4,
                        Qty5 = cutassign.SampCutReqMaster.Qty5,
                        Qty6 = cutassign.SampCutReqMaster.Qty6,
                        Size1CutQty = cutassign.SampCutReqMaster.Size1CutQty,
                        Size2CutQty = cutassign.SampCutReqMaster.Size2CutQty,
                        Size3CutQty = cutassign.SampCutReqMaster.Size3CutQty,
                        Size4CutQty = cutassign.SampCutReqMaster.Size4CutQty,
                        Size5CutQty = cutassign.SampCutReqMaster.Size5CutQty,
                        Size6CutQty = cutassign.SampCutReqMaster.Size6CutQty,
                        Size1SewQty = cutassign.SampCutReqMaster.Size1SewQty,
                        Size2SewQty = cutassign.SampCutReqMaster.Size2SewQty,
                        Size3SewQty = cutassign.SampCutReqMaster.Size3SewQty,
                        Size4SewQty = cutassign.SampCutReqMaster.Size4SewQty,
                        Size5SewQty = cutassign.SampCutReqMaster.Size5SewQty,
                        Size6SewQty = cutassign.SampCutReqMaster.Size6SewQty,
                        DateofAction = cutassign.SampCutReqMaster.DateofAction,
                        Size1DeliveredQty = cutassign.SampCutReqMaster.Size1DeliveredQty,
                        Size2DeliveredQty = cutassign.SampCutReqMaster.Size2DeliveredQty,
                        Size3DeliveredQty = cutassign.SampCutReqMaster.Size3DeliveredQty,
                        Size4DeliveredQty = cutassign.SampCutReqMaster.Size4DeliveredQty,
                        Size5DeliveredQty = cutassign.SampCutReqMaster.Size5DeliveredQty,
                        Size6DeliveredQty = cutassign.SampCutReqMaster.Size6DeliveredQty,
                        ReceivedDate = cutassign.ReceivedDate,
                        AssignedDate = cutassign.ReceivedDate,
                        SignedBYMaster = cutassign.SignedBYMaster,
                        SignedDate = cutassign.ReceivedDate,
                        CompletedDate = cutassign.ReceivedDate,
                        PatternReqDate = cutassign.ReceivedDate,
                        PatternCompletedDate = cutassign.ReceivedDate,
                        CompletedQty = cutassign.CompletedQty,
                        PendingReason = cutassign.PendingReason,
                        AssignmentRemark = cutassign.Remark,
                        MarkedCompletedBY = cutassign.MarkedCompletedBY,
                        ReceivedBy = cutassign.ReceivedBy,
                        pastternmaster = cutassign.PatternMaster.PaternMasterName,
                        BuyerName = cutassign.SampCutReqMaster.BuyerMaster.BuyerName,

                        Patternrefernce = cutassign.SampCutReqMaster.PatterRefMaster.PatterRefNum,
                        SampleType = cutassign.SampCutReqMaster.SampleType.SampleType1,
                        PatternStyle = cutassign.SampCutReqMaster.PatternStyle.StyleName

                    }).First();

            return q;
        }



    }
}
