using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebArtSampler.Models
{
    public class RequestViewModel :Models.SampCutReqMaster
    {
        public String Remark { get; set; }

        public String BuyerName1 { get; set; }
        public String Stylename1 { get; set; }
        public String PatterRefNum1 { get; set; }
        public String SampleTypename1 { get; set; }
       

        public int totalcut { get; set; }

        public int totalsew { get; set; }
        public int totalDelivered { get; set; }
        //public String MyProperty { get; set; }
        //public String MyProperty { get; set; }
        //public String MyProperty { get; set; }
    }
}