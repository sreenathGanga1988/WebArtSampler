using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebArtSampler.Areas.Planning.Controllers
{
    public class PlanningController : Controller
    {
        // GET: Planning/Planning
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RedirectToCutting()
        {
            return RedirectToAction("Index");
        }
    }
}