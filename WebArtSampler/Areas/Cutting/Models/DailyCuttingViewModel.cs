using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebArtSampler.Models;

namespace WebArtSampler.Areas.Cutting.Models
{
    public class DailyCuttingViewModel 
    {
        public DateTime DateofAction
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now.AddDays(-1);
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;
      
     
        public virtual ICollection<SamDailyCutStatu> SamDailyCutStatu { get; set; }
    }


    public class CuttingViewModel
    {


       
        [Required]
        public DateTime DateofAction { get; set; }

        public SamCutAssignmentMaster samcutAssignmentmaster { get; set; }

    }
    public class SewingViewModel
    {



        [Required]
        public DateTime DateofAction { get; set; }

        public SamCutAssignmentMaster samcutAssignmentmaster { get; set; }

    }

}