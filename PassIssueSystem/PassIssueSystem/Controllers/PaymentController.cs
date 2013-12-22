using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;
using PassIssueSystem.Twilio;

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
        // GET: /Payment/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Payment/Create

        public ActionResult Create()
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewBag.Company = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();
            return View();
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
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

        //
        // GET: /Payment/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Payment/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Payment/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Payment/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
