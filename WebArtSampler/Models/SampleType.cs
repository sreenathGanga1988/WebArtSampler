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
    
    public partial class SampleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SampleType()
        {
            this.SampCutReqMasters = new HashSet<SampCutReqMaster>();
        }
    
        public decimal SampleTypeID { get; set; }
        public string SampleType1 { get; set; }
        public string Description { get; set; }
        public string CuttingTicketFrom { get; set; }
        public Nullable<bool> NeedApproval { get; set; }
        public Nullable<decimal> SampleApprovalID { get; set; }
        public Nullable<bool> NeedReApproval { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SampCutReqMaster> SampCutReqMasters { get; set; }
    }
}
