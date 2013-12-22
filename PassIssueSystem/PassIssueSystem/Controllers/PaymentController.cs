using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;
using PassIssueSystem.Twilio;
using PassIssueSystem.Facades;

namespace PassIssueSystem.Controllers
{
    public class PaymentController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Payment/

        public ActionResult Index()
        {            
            return View();
        }

        //
        // GET: /Payment/Create

        public ActionResult Create(string ReqNo)
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewBag.Company = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();

            int Req = Convert.ToInt16(ReqNo);
            decimal PassTot = PaymentFacade.GetPassTotal(Req);
            ViewBag.Total = PassTot;
            
            return View();
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        public ActionResult Create(PaymentDetail paymentdetail)
        {
            try
            {
                // SMS Logic
                var client = new TwilioRestClient("AC73a7e5cc8ceb71599d77a664307425ee", "58f1829a5b6534d6acd3a25041742d17");
                var result = client.SendSmsMessage("+13312085208", "+94777006211", "Your Pass No : ");

                TempData["result"] = "SMS Status : " + result.Status;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Client(string ReqNo)
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewBag.Company = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();

            int Req = Convert.ToInt16(ReqNo);
            decimal PassTot = PaymentFacade.GetPassTotal(Req);
            ViewBag.Total = PassTot;

            return View();
        }

        /// <summary>
        /// Pays the pal.
        /// </summary>
        /// <returns></returns>
        public ActionResult PayPal()
        {
            return View();
        }
    }
}
