using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebArtSampler.Areas.Planning.Models
{
    public class MasterDailyPriority
    {
        public DateTime Fromdate { get; set; }
        public int PatternMasterID { get; set; }
        public List<RequestCuttingPriority> RequestCuttingPrioritylist { get; set; }
    }
}