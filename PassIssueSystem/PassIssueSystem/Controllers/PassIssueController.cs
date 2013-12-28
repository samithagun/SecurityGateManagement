using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;
using WebMatrix.WebData;

namespace PassIssueSystem.Controllers
{
    [Authorize]
    public class PassIssueController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /PassIssue/

        public ActionResult Index()
        {
            // Gets all the passes issued from the user
            return View(db.PassIssueHeds.ToList().Where(p => p.AddUser == WebSecurity.CurrentUserName));
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

        /// <summary>
        /// Saves the pass issue hed.
        /// </summary>
        /// <param name="Hed">The hed.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public int SavePassIssueHed(PassIssueHed Hed)
        {
            Hed.AddDate = DateTime.Now;
            Hed.AddUser = WebSecurity.CurrentUserName;

            if (ModelState.IsValid)
            {
                db.PassIssueHeds.Add(Hed);
                db.SaveChanges();
            }

            return Hed.PassNo;
        }

        /// <summary>
        /// Saves the pass issue det.
        /// </summary>
        /// <param name="passrequestdet">The passrequestdet.</param>
        /// <param name="refNo">The reference no.</param>
        public void SavePassIssueDet(List<PassIssueDet> passrequestdet, int refNo)
        {
            foreach (PassIssueDet Det in passrequestdet)
            {
                if (ModelState.IsValid)
                {
                    Det.PassNo = refNo;
                    db.PassIssueDets.Add(Det);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves the pass req vehi.
        /// </summary>
        /// <param name="passreqvehicle">The passreqvehicle.</param>
        /// <param name="refNo">The reference no.</param>
        public void SavePassIssueVehi(List<PassIssueVehicle> passreqvehicle, int refNo)
        {
            foreach (PassIssueVehicle Vehi in passreqvehicle)
            {
                if (ModelState.IsValid)
                {
                    Vehi.PassNo = refNo;
                    db.PassIssueVehicles.Add(Vehi);
                    db.SaveChanges();
                }
            }
        }
    }
}
