using System.Web.Mvc;

namespace WebArtSampler.Areas.ApprovalSample
{
    public class ApprovalSampleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ApprovalSample";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ApprovalSample_default",
                "ApprovalSample/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}