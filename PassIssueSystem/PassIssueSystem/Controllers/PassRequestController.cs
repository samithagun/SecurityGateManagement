using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            var passreqhed = db.PassRequestHeds.Include(p => p.Company);
            
            return View(passreqhed.ToList());
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
            return View();
        }

        /// <summary>
        /// Finds the specified nic.
        /// </summary>
        /// <param name="NIC">The nic.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Find(string NIC)
        {
            try
            {
                int Req = PassRequestFacade.GetRequestFromID(NIC);
                if (Req == 0)
                {
                    ModelState.AddModelError("ErrorMsg", "No Records Found");
                    return View();
                }

                return RedirectToAction("View", new { ReqNo = Req, ID = NIC });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorMsg", "Error: " + ex.ToString());
                return RedirectToAction("Find");
            }
        }

        /// <summary>
        /// Views the specified req no.
        /// </summary>
        /// <param name="ReqNo">The req no.</param>
        /// <param name="NICNo">The nic no.</param>
        /// <returns></returns>
        public ActionResult View(int ReqNo, String ID)
        {            
            IEnumerable<PassRequestHed> PRH;
            
            if (Roles.IsUserInRole("Pass Office"))
            {
                // Needs to filter with NIC No (ID == NIC No)
                PRH = db.PassRequestHeds.ToList().Where(p => p.Issued == false);
            }
            else
            {
                // Needs to filter with Company ID
                PRH = db.PassRequestHeds.ToList().Where(p => p.Paid == false && p.CompanyID == ID);    
            }
            
            if (PRH == null)
            {
                return HttpNotFound();
            }
            
            return View(PRH);
        }

    }
}