﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;

namespace PassIssueSystem.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(string id = null)
        {
            TempData.Keep("");
            Company company = db.Companies.Find(id);
            
            if (company == null)
            {
                return HttpNotFound();
            }

            ViewData["currentcompany"] = company;
            TempData["currentcompany"] = company;
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            company.AddDate = DateTime.Now;
            company.AddUser = User.Identity.Name;
            company.Status = 1;

            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                //TempData["UserMessage"] = "Data Saved Successfully";
                return RedirectToAction("Index");
            }

            return View(company);
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(string id = null)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            company.AddDate = DateTime.Now;
            company.AddUser = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(string id = null)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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