using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;

namespace PassIssueSystem.Controllers
{
    public class PassRequestController : Controller
    {
        private PassIssueSystemEntities db = new PassIssueSystemEntities();

        //
        // GET: /PassRequest/

        public ActionResult Index()
        {
            var passrequestheds = db.PassRequestHeds.Include(p => p.Company);
            return View(passrequestheds.ToList());
        }

        //
        // GET: /PassRequest/Details/5

        public ActionResult Details(string id = null)
        {
            PassRequestHed passrequesthed = db.PassRequestHeds.Find(id);
            if (passrequesthed == null)
            {
                return HttpNotFound();
            }
            return View(passrequesthed);
        }

        //
        // GET: /PassRequest/Create

        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "CompanyName");
            return View();
        }

        //
        // POST: /PassRequest/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PassRequestHed passrequesthed)
        {
            if (ModelState.IsValid)
            {
                db.PassRequestHeds.Add(passrequesthed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "CompanyName", passrequesthed.CompanyID);
            return View(passrequesthed);
        }

        //
        // GET: /PassRequest/Edit/5

        public ActionResult Edit(string id = null)
        {
            PassRequestHed passrequesthed = db.PassRequestHeds.Find(id);
            if (passrequesthed == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "CompanyName", passrequesthed.CompanyID);
            return View(passrequesthed);
        }

        //
        // POST: /PassRequest/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PassRequestHed passrequesthed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passrequesthed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "CompanyID", "CompanyName", passrequesthed.CompanyID);
            return View(passrequesthed);
        }

        //
        // GET: /PassRequest/Delete/5

        public ActionResult Delete(string id = null)
        {
            PassRequestHed passrequesthed = db.PassRequestHeds.Find(id);
            if (passrequesthed == null)
            {
                return HttpNotFound();
            }
            return View(passrequesthed);
        }

        //
        // POST: /PassRequest/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PassRequestHed passrequesthed = db.PassRequestHeds.Find(id);
            db.PassRequestHeds.Remove(passrequesthed);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}