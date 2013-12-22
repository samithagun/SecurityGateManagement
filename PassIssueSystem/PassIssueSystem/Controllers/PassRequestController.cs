using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Facades;
using PassIssueSystem.Filters;
using PassIssueSystem.Models;
using WebMatrix.WebData;

namespace PassIssueSystem.Controllers
{
    [Authorize]
    //[InitializeSimpleMembership]
    public class PassRequestController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /PassRequest/

        public ActionResult Index()
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewBag.Company = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();

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
            ViewBag.PassCode = new SelectList(db.PassTypes, "PassCode", "Description");
            ViewBag.VehicleCode = new SelectList(db.VehicleTypes, "VehicleCode", "Description");
            
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewBag.Company = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();
            
            return View();
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


        /// <summary>
        /// Jsons the create request.
        /// </summary>
        /// <param name="passrequesthed">The passrequesthed.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult JsonCreateRequest(PassRequestHed passReq)
        {
            int Ref = 0;

            try
            { 
                 Ref = PassRequestFacade.SavePassRequest(passReq);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return Json(new { refNo = Ref });
        }


        /// <summary>
        /// Creates the specified passrequesthed.
        /// </summary>
        /// <param name="passrequesthed">The passrequesthed.</param>
        /// <param name="passrequestdet">The passrequestdet.</param>
        /// <param name="passreqvehicle">The passreqvehicle.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public int SavePassReqHed(PassRequestHed Hed)
        {
            Hed.AddDate = DateTime.Now;
            Hed.AddUser = WebSecurity.CurrentUserName;
            Hed.CompanyID = db.UserProfiles.Where(u => u.UserName == WebSecurity.CurrentUserName).Select(s => s.CompanyID).First();
            Hed.Issued = false;
            Hed.Paid = false;

            if (ModelState.IsValid)
            {
                db.PassRequestHeds.Add(Hed);
                db.SaveChanges();
            }

            return Hed.PassReqNo;
        }

        /// <summary>
        /// Saves the pass req det.
        /// </summary>
        /// <param name="passrequestdet">The passrequestdet.</param>
        /// <param name="refNo">The reference no.</param>
        public void SavePassReqDet(List<PassRequestDet> passrequestdet, int refNo)
        {
            foreach (PassRequestDet Det in passrequestdet)
            {
                if (ModelState.IsValid)
                {
                    Det.PassReqNo = refNo;
                    db.PassRequestDets.Add(Det);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Saves the pass req vehi.
        /// </summary>
        /// <param name="passreqvehicle">The passreqvehicle.</param>
        /// <param name="refNo">The reference no.</param>
        public void SavePassReqVehi(List<PassReqVehicle> passreqvehicle, int refNo)
        {
            foreach (PassReqVehicle Vehi in passreqvehicle)
            {
                if (ModelState.IsValid)
                {
                    Vehi.PassReqNo = refNo;
                    db.PassReqVehicles.Add(Vehi);
                    db.SaveChanges();
                }
            }
        }

        [HttpGet]
        public ActionResult Find()
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewBag.Company = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();

            return View();
        }

        [HttpPost]
        public ActionResult Find(string NICNo)
        {
            try
            {
                int Req = PassRequestFacade.GetRequestFromID(NICNo);
                if (Req == 0)
                {
                    ModelState.AddModelError("ErrorMsg", "No Records Found");
                }

                return RedirectToAction("View", new { ReqNo = Req });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorMsg", "No Records Found");
                return RedirectToAction("Find");
            }
        }

        public ActionResult View(int ReqNo)
        {
            PassRequestHed PRH = db.PassRequestHeds.Find(ReqNo);
            
            if (PRH == null)
            {
                return HttpNotFound();
            }
            
            return View(PRH);
        }
    }
}