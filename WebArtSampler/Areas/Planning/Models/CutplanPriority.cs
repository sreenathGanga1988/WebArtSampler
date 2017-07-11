using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Planning.Models
{
    public class CutplanPriority
    {
        public DateTime Fromdate { get; set; }
        public List<RequestCuttingPriority> RequestCuttingPrioritylist { get; set; }
    }

    

    
}