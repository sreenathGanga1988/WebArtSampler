using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebArtSampler.Areas.Cutting.Models
{

    public class IPOListViewModel
    {

        public List <IPOViewModel> IPOMasterlist { get; set; }
    }


    public class IPOViewModel
    {

        public String OODoLocation { get; set; }
        public String IPONumber { get; set; }


        public List<IPODetailsViewModel> IPODetailsViewModellist { get; set; }
    }

    public class IPODetailsViewModel
    {
        public String ItemDescription { get; set; }

        public String RequiredQty { get; set; }

        public String OODOUOM { get; set; }
        public List<SpoDetails> SpoDetailsList { get; set; }

    }

    public class SpoDetails
    {
        public String SPOnum { get; set; }

        public String  Qty { get; set; }

        public String AddedDate { get; set; }

        public String Suppplier { get; set; }

        public String SPOUOM { get; set; }

        public List<SMRNDetails> SMRNDetailsList { get; set; }
    }



    public class SMRNDetails
    {
        public String SMRNNUM { get; set; }

        public String Qty { get; set; }

        public String ExtraQty { get; set; }

        public String SPOnum { get; set; }

        public String SMRNDate { get; set; }
        public List<SDODetails> SDODetailsList { get; set; }
    }

    public class SDODetails
    {
        public String SDONUM { get; set; }

        public String Qty { get; set; }

        public String AddedBy { get; set; }

        public String SPOnum { get; set; }

        public String SMRNDate { get; set; }

    }

}