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
    public class SampCutReqMastersController : Controller
    {
        private ArtEntities db = new ArtEntities();




    // GET: SampCutReqMasters
    public ActionResult Index()
        {
            var sampCutReqMasters = db.SampCutReqMasters.Include(s => s.BuyerMaster).Include(s => s.PatternStyle).Include(s => s.PatterRefMaster).Include(s => s.SampleType);

          
            var sampCutReqMasterssort = sampCutReqMasters.ToList().OrderByDescending(a => a.AddedDate);
            return View(sampCutReqMasterssort.ToList());
        }




        // GET: SampCutReqMasters
        public ActionResult ShowCutreqStatus()
        {

            try
            {
                ViewBag.SuccessMessage = TempData["shortMessage"].ToString();
            }
            catch (Exception)
            {


            }
            var sampCutReqMasters = db.SampCutReqMasters.Include(s => s.BuyerMaster).Include(s => s.PatternStyle).Include(s => s.PatterRefMaster).Include(s => s.SampleType);


            List<RequestViewModel> rvmdellist = new List<Models.RequestViewModel>() ;
            var q = from ast in db.SampCutReqMasters
                    where ast.MarkCompleted == null
                    orderby ast.AddedDate descending
                    select new
                    {
                        ast.SampCutreqID,
                        ast.ReqNum,
                        ast.Fabric,
                        ast.SampleRequiredDate,
                        ast.AddedDate,
                        ast.AddedBy,
                        ast.BuyerMaster.BuyerName,
                        ast.PatternStyle.StyleName,
                        ast.PatterRefMaster.PatterRefNum,
                        ast.SampleType.SampleType1,
                        ast.SizeDetail,
                        ast.Qty,
      ast.Size1CutQty,
      ast.Size2CutQty,
      ast.Size3CutQty,
      ast.Size4CutQty,
      ast.Size5CutQty,
      ast.Size6CutQty,
      ast.Size1SewQty,
      ast.Size2SewQty,
      ast.Size3SewQty,
      ast.Size4SewQty,
      ast.Size5SewQty,
      ast.Size6SewQty,   
     
      ast.Size1DeliveredQty,
      ast.Size2DeliveredQty,
      ast.Size3DeliveredQty,
      ast.Size4DeliveredQty,
      ast.Size5DeliveredQty,
      ast.Size6DeliveredQty
                                      
                                 


    };
            foreach (var element in q)
            {
                RequestViewModel vmdel = new RequestViewModel();
                vmdel.SampCutreqID = element.SampCutreqID;
                vmdel.ReqNum = element.ReqNum;
                vmdel.Fabric = element.Fabric;
                vmdel.SampleRequiredDate = element.SampleRequiredDate;
                vmdel.AddedDate = element.AddedDate;
                vmdel.AddedBy = element.AddedBy;
                vmdel.BuyerName1= element.BuyerName;
                
                vmdel.Stylename1 = element.StyleName;
                vmdel.PatterRefNum1 = element.PatterRefNum;
                vmdel.SampleTypename1 = element.SampleType1;
                vmdel.SizeDetail = element.SizeDetail;
                vmdel.Qty = element.Qty;

                var prefix = db.SamCutAssignmentMasters.Where(u => u.SampCutreqID == element.SampCutreqID).Select(u => u.Remark).FirstOrDefault();

                if(prefix==null)
                {
                    prefix = "NA";
                }
                else
                {

                }



                int SizeQty1 = element.Size1CutQty.Equals(null) ? 0 : Convert.ToInt32(element.Size1CutQty.ToString());
                int SizeQty2 = element.Size2CutQty.Equals(null) ? 0 : Convert.ToInt32(element.Size2CutQty.ToString());
                int SizeQty3 = element.Size3CutQty.Equals(null) ? 0 : Convert.ToInt32(element.Size3CutQty.ToString());
                int SizeQty4 = element.Size4CutQty.Equals(null) ? 0 : Convert.ToInt32(element.Size4CutQty.ToString());
                int SizeQty5 = element.Size5CutQty.Equals(null) ? 0 : Convert.ToInt32(element.Size5CutQty.ToString());
                int SizeQty6 = element.Size6CutQty.Equals(null) ? 0 : Convert.ToInt32(element.Size6CutQty.ToString());

                vmdel. totalcut = SizeQty1 + SizeQty2 + SizeQty3 + SizeQty4 + SizeQty5 + SizeQty6;


                int sewQty1 = element.Size1SewQty.Equals(null) ? 0 : Convert.ToInt32(element.Size1SewQty.ToString());
                int sewQty2 = element.Size2SewQty.Equals(null) ? 0 : Convert.ToInt32(element.Size2SewQty.ToString());
                int sewQty3 = element.Size3SewQty.Equals(null) ? 0 : Convert.ToInt32(element.Size3SewQty.ToString());
                int sewQty4 = element.Size4SewQty.Equals(null) ? 0 : Convert.ToInt32(element.Size4SewQty.ToString());
                int sewQty5 = element.Size5SewQty.Equals(null) ? 0 : Convert.ToInt32(element.Size5SewQty.ToString());
                int sewQty6 = element.Size6SewQty.Equals(null) ? 0 : Convert.ToInt32(element.Size6SewQty.ToString());

                vmdel.totalsew = sewQty1 + sewQty2 + sewQty3 + sewQty4 + sewQty5 + sewQty6;


                int deliveredQty1 = element.Size1DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.Size1DeliveredQty.ToString());
                int deliveredQty2 = element.Size2DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.Size2DeliveredQty.ToString());
                int deliveredQty3 = element.Size3DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.Size3DeliveredQty.ToString());
                int deliveredQty4 = element.Size4DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.Size4DeliveredQty.ToString());
                int deliveredQty5 = element.Size5DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.Size5DeliveredQty.ToString());
                int deliveredQty6 = element.Size6DeliveredQty.Equals(null) ? 0 : Convert.ToInt32(element.Size6DeliveredQty.ToString());
                
                vmdel.totalDelivered = deliveredQty1 + deliveredQty2 + deliveredQty3 + deliveredQty4 + deliveredQty5 + deliveredQty6;





                vmdel.Remark = prefix.ToString();
                rvmdellist.Add(vmdel);
            }

            var sampCutReqMasterssort = rvmdellist.ToList().OrderByDescending(a => a.AddedDate);
            return View(rvmdellist);
        }

        // GET: SampCutReqMasters/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampCutReqMaster sampCutReqMaster = db.SampCutReqMasters.Find(id);
            if (sampCutReqMaster == null)
            {
                return HttpNotFound();
            }
            return View(sampCutReqMaster);
        }


        // GET: SampCutReqMasters/Complete/5
        public ActionResult Complete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestViewModel vmdel = new RequestViewModel();
            var q = from ast in db.SampCutReqMasters
                    orderby ast.AddedDate descending
                    where ast.SampCutreqID==id
                    select new
                    {
                        ast.SampCutreqID,
                        ast.ReqNum,
                        ast.Fabric,
                        ast.SampleRequiredDate,
                        ast.AddedDate,
                        ast.AddedBy,
                        ast.BuyerMaster.BuyerName,
                        ast.PatternStyle.StyleName,
                        ast.PatterRefMaster.PatterRefNum,
                        ast.SampleType.SampleType1,
                        ast.SizeDetail,
                        ast.Qty
                    };



            foreach (var element in q)
            {
               
                vmdel.SampCutreqID = element.SampCutreqID;
                vmdel.ReqNum = element.ReqNum;
                vmdel.Fabric = element.Fabric;
                vmdel.SampleRequiredDate = element.SampleRequiredDate;
                vmdel.AddedDate = element.AddedDate;
                vmdel.AddedBy = element.AddedBy;
                vmdel.BuyerName1 = element.BuyerName;

                vmdel.Stylename1 = element.StyleName;
                vmdel.PatterRefNum1 = element.ReqNum;
                vmdel.SampleTypename1 = element.SampleType1;
                vmdel.SizeDetail = element.SizeDetail;
                vmdel.Qty = element.Qty;

                var prefix = db.SamCutAssignmentMasters.Where(u => u.SampCutreqID == element.SampCutreqID).Select(u => u.Remark).FirstOrDefault();

                if (prefix == null)
                {
                    prefix = "NA";
                }
                else
                {

                }
                vmdel.Remark = prefix.ToString();
               
            }

            if (vmdel == null)
            {
                return HttpNotFound();
            }
            return View(vmdel);
        }

        // POST: SampCutReqMasters/Complete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompleteDetail(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var q = from ast in db.SampCutReqMasters
                    where ast.SampCutreqID == id
                    select ast;

               


            foreach (var element in q)
            {
                element.MarkCompleted = true;
                element.MarkedCompletedDate = DateTime.Now;
              
            }
            db.SaveChanges();
           
            return RedirectToAction("ShowCutreqStatus");
        }


        // GET: SampCutReqMasters/Create
        public ActionResult Create()
        {
            ViewBag.DefaultDescription = "TBA";
            ViewBag.addeddate = DateTime.Now.ToString ();
            
            //ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName");
            ViewBag.BuyerID = new SelectList(db.BuyerMasters.Where(o => o.IsActive == "Y"), "BuyerID", "BuyerName");
            ViewBag.PatternStyleID = new SelectList(db.PatternStyles, "PatternStyleID", "StyleName");
            ViewBag.PatternRefID = new SelectList(db.PatterRefMasters, "PatternRefID", "PatterRefNum");
            ViewBag.SampleTypeID = new SelectList(db.SampleTypes.Where(o => o.IsActive == true), "SampleTypeID", "SampleType1");
            ViewBag.Fabric = new SelectList(db.SamplingFabricMasters.Where(o => o.ISDeleted == "N"), "Fabric", "Fabric");


            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SampCutreqID,ReqNum,Fabric,StyleDescription,BuyerID,PatternRefID,PatternStyleID,SampleTypeID,SampleRequiredDate,AddedDate,AddedBy,SizeDetail,Qty,IsTeckPack, Size1, Size2, Size3, Size4, Size5, Size6, Qty1, Qty2, Qty3, Qty4, Qty5, Qty6")] SampCutReqMaster sampCutReqMaster)
        {     
            if (ModelState.IsValid)
            {
                if (sampCutReqMaster.ReqNum != ""&& sampCutReqMaster.ReqNum!=null)
                {

                    if (db.SampCutReqMasters.Any(o => o.ReqNum == sampCutReqMaster.ReqNum))
                    {
                        sampCutReqMaster.ReqNum = "";
                    }
                    else
                    {

                    }


                    sampCutReqMaster.IsReceived = "N";
                    sampCutReqMaster.Size1SewQty = 0;
                    sampCutReqMaster.Size2SewQty = 0;
                    sampCutReqMaster.Size3SewQty = 0;
                    sampCutReqMaster.Size4SewQty = 0;
                    sampCutReqMaster.Size5SewQty = 0;
                    sampCutReqMaster.Size6SewQty = 0;

                    sampCutReqMaster.Size1CutQty = 0;
                    sampCutReqMaster.Size2CutQty = 0;
                    sampCutReqMaster.Size3CutQty = 0;
                    sampCutReqMaster.Size4CutQty = 0;
                    sampCutReqMaster.Size5CutQty = 0;
                    sampCutReqMaster.Size6CutQty = 0;
                    sampCutReqMaster.AddedDate = DateTime.Now;
                    db.SampCutReqMasters.Add(sampCutReqMaster);
                    db.SaveChanges();

                    if (sampCutReqMaster.ReqNum=="" || sampCutReqMaster.ReqNum=="TBA")
                    {
                        sampCutReqMaster.ReqNum= "SR" + ((int.Parse(sampCutReqMaster.SampCutreqID.ToString()) ) + 1000).ToString();
                        db.SaveChanges();
                    }

                    TempData["shortMessage"] = "Cutting Ticket#" + sampCutReqMaster.ReqNum.ToString() + "Created Successfully";
                        return RedirectToAction("ShowCutreqStatus");
                }

            }
            ViewBag.BuyerID = new SelectList(db.BuyerMasters.Where(o => o.IsActive == "Y"), "BuyerID", "BuyerName", sampCutReqMaster.BuyerID);
            //ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", sampCutReqMaster.BuyerID);
            ViewBag.PatternStyleID = new SelectList(db.PatternStyles, "PatternStyleID", "StyleName", sampCutReqMaster.PatternStyleID);
            ViewBag.PatternRefID = new SelectList(db.PatterRefMasters, "PatternRefID", "PatterRefNum", sampCutReqMaster.PatternRefID);
            ViewBag.SampleTypeID = new SelectList(db.SampleTypes.Where(o => o.IsActive == true), "SampleTypeID", "SampleType1", sampCutReqMaster.SampleTypeID);

            ViewBag.Fabric = new SelectList(db.SamplingFabricMasters.Where(o => o.ISDeleted == "N"), "Fabric", "Fabric", sampCutReqMaster.Fabric);

            return View(sampCutReqMaster);
        }



        // GET: SampCutReqMasters/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampCutReqMaster sampCutReqMaster = db.SampCutReqMasters.Find(id);
            if (sampCutReqMaster == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BuyerID = new SelectList(db.BuyerMasters, "BuyerID", "BuyerName", sampCutReqMaster.BuyerID);
            ViewBag.BuyerID = new SelectList(db.BuyerMasters.Where(o => o.IsActive == "Y"), "BuyerID", "BuyerName", sampCutReqMaster.BuyerID);
            ViewBag.PatternStyleID = new SelectList(db.PatternStyles, "PatternStyleID", "StyleName", sampCutReqMaster.PatternStyleID);
            ViewBag.PatternRefID = new SelectList(db.PatterRefMasters, "PatternRefID", "PatterRefNum", sampCutReqMaster.PatternRefID);
          //  ViewBag.SampleTypeID = new SelectList(db.SampleTypes, "SampleTypeID", "SampleType1", sampCutReqMaster.SampleTypeID);
            ViewBag.SampleTypeID = new SelectList(db.SampleTypes.Where(o => o.IsActive == true), "SampleTypeID", "SampleType1", sampCutReqMaster.SampleTypeID);
            return View(sampCutReqMaster);
        }
    
        
        // POST: SampCutReqMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SampCutreqID,ReqNum,Fabric,StyleDescription,BuyerID,PatternRefID,PatternStyleID,SampleTypeID,SampleRequiredDate,AddedDate,AddedBy,SizeDetail,Qty,Remark,IsTeckPack,Size1, Size2, Size3, Size4, Size5, Size6, Qty1, Qty2, Qty3, Qty4, Qty5, Qty6")] SampCutReqMaster sampCutReqMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sampCutReqMaster).State = EntityState.Modified;
                db.Entry(sampCutReqMaster).Property(x => x.IsReceived).IsModified = false;
                Feildsnonchangable(sampCutReqMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerID = new SelectList(db.BuyerMasters.Where(o => o.IsActive == "Y"), "BuyerID", "BuyerName", sampCutReqMaster.BuyerID);
            ViewBag.PatternStyleID = new SelectList(db.PatternStyles, "PatternStyleID", "StyleName", sampCutReqMaster.PatternStyleID);
            ViewBag.PatternRefID = new SelectList(db.PatterRefMasters, "PatternRefID", "PatterRefNum", sampCutReqMaster.PatternRefID);
            ViewBag.SampleTypeID = new SelectList(db.SampleTypes.Where(o => o.IsActive == true), "SampleTypeID", "SampleType1", sampCutReqMaster.SampleTypeID);
            return View(sampCutReqMaster);
        }

        public void Feildsnonchangable(SampCutReqMaster sampCutReqMaster)
        {
         
            db.Entry(sampCutReqMaster).Property(x => x.Size1CutQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size1SewQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size2CutQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size2SewQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size3CutQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size3SewQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size4CutQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size4SewQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size5CutQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size5SewQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size6CutQty).IsModified = false;
            db.Entry(sampCutReqMaster).Property(x => x.Size6SewQty).IsModified = false;
        }


        // GET: SampCutReqMasters/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SampCutReqMaster sampCutReqMaster = db.SampCutReqMasters.Find(id);
            if (sampCutReqMaster == null)
            {
                return HttpNotFound();
            }
            return View(sampCutReqMaster);
        }

        // POST: SampCutReqMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SampCutReqMaster sampCutReqMaster = db.SampCutReqMasters.Find(id);
            db.SampCutReqMasters.Remove(sampCutReqMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public string getAutomaticnumber()
        {
            String atcnum = "";
            //var count = (from o in db.SampCutReqMasters

            //                  select o).Count();
            Decimal count = db.SampCutReqMasters.Max(p => p.SampCutreqID);
            atcnum = "SR" + ((int.Parse(count.ToString())+1) + 1000).ToString();

            if (db.SampCutReqMasters.Any(o => o.ReqNum == atcnum))
            {
                atcnum = "TBA";
            }
            return atcnum;
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
