using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;

namespace PassIssueSystem.Controllers
{
     [Authorize]
    public class PaymentController : Controller
    {

        private PassIssueSystemEntities db = new PassIssueSystemEntities();

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
            return View();
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentDetail paymentdetail)
        {
            if (ModelState.IsValid)
            {
                db.PaymentDetails.Add(paymentdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentdetail);
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
