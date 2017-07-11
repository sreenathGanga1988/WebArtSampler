using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebArtSampler.Models
{
    public class RequestSignModel
    {
       
        public int CutAssignID { get; set; }
        public int cutreqid { get; set; }
        public Boolean SignedBYMaster { get; set; }
        public DateTime SignedDate { get; set; }
       
        public String ReqNum { get; set; }
        public String BuyerName { get; set; }
        public String PatterRefNum { get; set; }
        public String StyleName { get; set; }
        public String StyleDescription { get; set; }
        public String Fabric { get; set; }
        public String SampleType { get; set; }
        public String SampleRequiredDate { get; set; }
        public String PaternMasterName { get; set; }
        public String AddedBy { get; set; }
        public String PatternReqDateString { get; set; }
        public DateTime PatternReqDate { get; set; }
        // public int SignedDate { get; set; }
        

         public List<Requestnumber> Reqnumlist { get; set; }
    }



    public class Requestnumber
    {
        public Decimal CutAssignID { get; set; }
        public String ReqNum { get; set; }
    }


    public class AssignRequestModel
    {
        public Decimal CutAssignID { get; set; }
        public String ReqNum { get; set; }
        public int Priority { get; set; }
        public List<RequestSignModel> Reqnumlist { get; set; }
    }
}