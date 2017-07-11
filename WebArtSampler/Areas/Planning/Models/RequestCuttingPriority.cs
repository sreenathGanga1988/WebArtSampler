using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Planning.Models
{
    public class RequestCuttingPriority : SamplingView
    {
        public bool isSelected { get; set; }
        public string samreqdate { get; set; }
        public string AddedBy { get; set; }
        public int Priority { get; set; }
        public int NewQty { get; set; }
        public Decimal CuttingAssignId { get; set; }
    }
}