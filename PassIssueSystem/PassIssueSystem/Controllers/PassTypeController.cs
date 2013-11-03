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
    public class PassTypeController : Controller
    {
        private PassIssueSystemEntities db = new PassIssueSystemEntities();

        //
        // GET: /PassType/

        public ActionResult Index()
        {
            return View(db.PassTypes.ToList());
        }

        //
        // GET: /PassType/Details/5

        public ActionResult Details(string id = null)
        {
            PassType passtype = db.PassTypes.Find(id);
            if (passtype == null)
            {
                return HttpNotFound();
            }
            return View(passtype);
        }

        //
        // GET: /PassType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PassType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PassType passtype)
        {
            if (ModelState.IsValid)
            {
                db.PassTypes.Add(passtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(passtype);
        }

        //
        // GET: /PassType/Edit/5

        public ActionResult Edit(string id = null)
        {
            PassType passtype = db.PassTypes.Find(id);
            if (passtype == null)
            {
                return HttpNotFound();
            }
            return View(passtype);
        }

        //
        // POST: /PassType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PassType passtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passtype);
        }

        //
        // GET: /PassType/Delete/5

        public ActionResult Delete(string id = null)
        {
            PassType passtype = db.PassTypes.Find(id);
            if (passtype == null)
            {
                return HttpNotFound();
            }
            return View(passtype);
        }

        //
        // POST: /PassType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PassType passtype = db.PassTypes.Find(id);
            db.PassTypes.Remove(passtype);
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