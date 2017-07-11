using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Planning.Models
{
    public class DailyCutting
    {
        public int SizeQty1 { get; set; }
        public int SizeQty2 { get; set; }
        public int SizeQty3 { get; set; }
        public int SizeQty4 { get; set; }
        public int SizeQty5 { get; set; }
        public int SizeQty6 { get; set; }
        public String SizeName1 { get; set; }
        public String SizeName2 { get; set; }
        public String SizeName3 { get; set; }
        public String SizeName4 { get; set; }
        public String SizeName5 { get; set; }
        public String Sizename6 { get; set; }
        public SampCutReqMaster sampCutReqMasterdet { get; set; }
    }
}