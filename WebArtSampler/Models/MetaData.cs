using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebArtSampler.Models
{

    public class SampCutReqMasterMetaData
    {
       
    

        public decimal SampCutreqID { get; set; }

        [StringLength(50)]
        [Display(Name = "Cutting Ticket")]
        public string ReqNum { get; set; }
        [Display(Name = "Fabric To Use")]
        public string Fabric { get; set; }
        public string StyleDescription { get; set; }
        [Display(Name = "Buyer")]
        public Nullable<decimal> BuyerID { get; set; }

        [Display(Name = "Pattern")]
        public Nullable<decimal> PatternRefID { get; set; }

        [Display(Name = "Style")]
        public Nullable<decimal> PatternStyleID { get; set; }
        public Nullable<decimal> SampleTypeID { get; set; }

        [Display(Name = "Sample Req Date")]
        public Nullable<System.DateTime> SampleRequiredDate { get; set; }
        [Display(Name = "Added Date")]
        public Nullable<System.DateTime> AddedDate { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Size")]
        public string SizeDetail { get; set; }
        [Display(Name = "Total Qty")]
        [Required(ErrorMessage = "Total Qty is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Total Qty must be a positive number")]
        public Nullable<decimal> Qty { get; set; }
        public Nullable<bool> MarkCompleted { get; set; }
        [Display(Name = "Marked Completed Date")]
        public Nullable<System.DateTime> MarkedCompletedDate { get; set; }
        [Display(Name = "Marked Completed By")]
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
    }
    public class SamCutAssignmentMasterMetaData
    {
        [Display(Name = "Received On")]
        public System.DateTime ReceivedDate
        {
            get; set;
        }

        public decimal CutAssignID { get; set; }
        public decimal SampCutreqID { get; set; }
     
        public string ReceivedBy { get; set; }
        public Nullable<decimal> PatternMasterID { get; set; }
        [Display(Name = "Assigned On")]
        public Nullable<System.DateTime> AssignedDate { get; set; }

        [Display(Name = "Master Signed")]
        public bool SignedBYMaster { get; set; }
        [Display(Name = "Master Signed Date")]
        public Nullable<System.DateTime> SignedDate { get; set; }
        [Display(Name = "Completed Date")]
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public string Remark { get; set; }
        [Display(Name = "Marked Completed By")]
        public string MarkedCompletedBY { get; set; }
        [Display(Name = "Pattern Req Date")]
        public Nullable<System.DateTime> PatternReqDate { get; set; }
        [Display(Name = "Pattern Completed Date")]
        public Nullable<System.DateTime> PatternCompletedDate { get; set; }
        public Nullable<decimal> SamplingStatus_PK { get; set; }
        [Display(Name = "Completed Qty")]
        public Nullable<decimal> CompletedQty { get; set; }
        [Display(Name = "Pending Reason")]
        public string PendingReason { get; set; }
    }
}