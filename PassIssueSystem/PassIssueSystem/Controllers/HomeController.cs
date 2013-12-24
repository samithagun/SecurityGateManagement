using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Filters;
using PassIssueSystem.Models;

namespace PassIssueSystem.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();     
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Client()
        {            
            var comId = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();              
            Session["Company"] = db.Companies.Where(c => c.CompanyID == comId).Select(n => n.CompanyName).First().ToString();
            Session["CompanyID"] = comId;

            return View();
        }

        public ActionResult Office()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}
