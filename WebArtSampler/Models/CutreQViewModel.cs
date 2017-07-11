using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebArtSampler.Models
{
    public class CutreQViewModel
    {
        public decimal SampCutreqID { get; set; }
        public string ReqNum { get; set; }
        public string Fabric { get; set; }
        public string StyleDescription { get; set; }       
        public Nullable<System.DateTime> SampleRequiredDate { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string SizeDetail { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<bool> MarkCompleted { get; set; }
        public Nullable<System.DateTime> MarkedCompletedDate { get; set; }
        public string MarkCompletedBy { get; set; }
        public string Remark { get; set; }
        public bool IsTeckPack { get; set; }
        public string IsReceived { get; set; }
        public string Size1 { get; set; }
        public string Size2 { get; set; }
        public string Size3 { get; set; }
        public string Size4 { get; set; }
        public string Size5 { get; set; }
        public string Size6 { get; set; }
        public Nullable<decimal> Qty1 { get; set; }
        public Nullable<decimal> Qty2 { get; set; }
        public Nullable<decimal> Qty3 { get; set; }
        public Nullable<decimal> Qty4 { get; set; }
        public Nullable<decimal> Qty5 { get; set; }
        public Nullable<decimal> Qty6 { get; set; }
        public Nullable<decimal> Size1CutQty { get; set; }
        public Nullable<decimal> Size2CutQty { get; set; }
        public Nullable<decimal> Size3CutQty { get; set; }
        public Nullable<decimal> Size4CutQty { get; set; }
        public Nullable<decimal> Size5CutQty { get; set; }
        public Nullable<decimal> Size6CutQty { get; set; }
        public Nullable<decimal> Size1SewQty { get; set; }
        public Nullable<decimal> Size2SewQty { get; set; }
        public Nullable<decimal> Size3SewQty { get; set; }
        public Nullable<decimal> Size4SewQty { get; set; }
        public Nullable<decimal> Size5SewQty { get; set; }
        public Nullable<decimal> Size6SewQty { get; set; }
        public Nullable<System.DateTime> DateofAction { get; set; }
        public Nullable<decimal> Size1DeliveredQty { get; set; }
        public Nullable<decimal> Size2DeliveredQty { get; set; }
        public Nullable<decimal> Size3DeliveredQty { get; set; }
        public Nullable<decimal> Size4DeliveredQty { get; set; }
        public Nullable<decimal> Size5DeliveredQty { get; set; }
        public Nullable<decimal> Size6DeliveredQty { get; set; }     
      
        public System.DateTime ReceivedDate { get; set; }
        public string ReceivedBy { get; set; }       
        public Nullable<System.DateTime> AssignedDate { get; set; }
        public bool SignedBYMaster { get; set; }
        public Nullable<System.DateTime> SignedDate { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string AssignmentRemark { get; set; }
        public string MarkedCompletedBY { get; set; }
        public Nullable<System.DateTime> PatternReqDate { get; set; }
        public Nullable<System.DateTime> PatternCompletedDate { get; set; }      
        public Nullable<decimal> CompletedQty { get; set; }
        public string PendingReason { get; set; }

        public string pastternmaster { get; set; }
        public string BuyerName { get; set; }

        public string Patternrefernce { get; set; }
        public string SampleType { get; set; }
        public string PatternStyle { get; set; }
        
    }
}