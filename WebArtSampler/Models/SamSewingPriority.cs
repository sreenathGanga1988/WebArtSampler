//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebArtSampler.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SamSewingPriority
    {
        public decimal SamSewPriorityID { get; set; }
        public Nullable<System.DateTime> PlanDate { get; set; }
        public Nullable<decimal> CutAssignID { get; set; }
        public Nullable<decimal> Priority { get; set; }
        public Nullable<decimal> ToSewQty { get; set; }
        public Nullable<decimal> ActualSew { get; set; }
    
        public virtual SamCutAssignmentMaster SamCutAssignmentMaster { get; set; }
    }
}
