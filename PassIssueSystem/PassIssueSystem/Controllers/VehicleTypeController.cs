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
    public class VehicleTypeController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /VehicleType/

        public ActionResult Index()
        {
            return View(db.VehicleTypes.ToList());
        }

        //
        // GET: /VehicleType/Details/5

        public ActionResult Details(string id = null)
        {
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            if (vehicletype == null)
            {
                return HttpNotFound();
            }
            return View(vehicletype);
        }

        //
        // GET: /VehicleType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VehicleType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleType vehicletype)
        {
            vehicletype.AddDate = DateTime.Now;
            vehicletype.AddUser = User.Identity.Name;
            vehicletype.Status = 1;

            if (ModelState.IsValid)
            {
                db.VehicleTypes.Add(vehicletype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicletype);
        }

        //
        // GET: /VehicleType/Edit/5

        public ActionResult Edit(string id = null)
        {
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            if (vehicletype == null)
            {
                return HttpNotFound();
            }
            return View(vehicletype);
        }

        //
        // POST: /VehicleType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehicleType vehicletype)
        {
            vehicletype.AddDate = DateTime.Now;
            vehicletype.AddUser = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Entry(vehicletype).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.successMessage = "Data Saved Successfully";
                return RedirectToAction("Index");
            }
            return View(vehicletype);
        }

        //
        // GET: /VehicleType/Delete/5

        public ActionResult Delete(string id = null)
        {
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            if (vehicletype == null)
            {
                return HttpNotFound();
            }
            return View(vehicletype);
        }

        //
        // POST: /VehicleType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VehicleType vehicletype = db.VehicleTypes.Find(id);
            db.VehicleTypes.Remove(vehicletype);
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