using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PassIssueSystem.Controllers
{
    public class PassIssueController : Controller
    {
        //
        // GET: /PassIssue/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PassIssue/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /PassIssue/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PassIssue/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PassIssue/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /PassIssue/Edit/5

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
        // GET: /PassIssue/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /PassIssue/Delete/5

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
