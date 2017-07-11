using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebArtSampler.Models
{
    [MetadataType(typeof(SampCutReqMasterMetaData))]
    public partial class SampCutReqMaster
    {
        
    }

    [MetadataType(typeof(SamCutAssignmentMasterMetaData))]
    public partial class SamCutAssignmentMaster
    {

        public int SizeQty1CutNew { get; set; }
        public int SizeQty2CutNew { get; set; }
        public int SizeQty3CutNew { get; set; }
        public int SizeQty4CutNew { get; set; }
        public int SizeQty5CutNew { get; set; }
        public int SizeQty6CutNew { get; set; }



        public int TotalCut { get; set; }
        public int TotalSew { get; set; }
        public int TotalDeliver { get; set; }

        public int Balancetocut { get; set; }
        public int BalanceToSew { get; set; }
        public int Balancetodeliver { get; set; }

      

        private int cutqty;

        public int ctotalSew;
        public int ctotalDelivery;
     
    }
}