using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PassIssueSystem.Models;
using PassIssueSystem.Twilio;
using PassIssueSystem.Facades;
using System.Transactions;
using System.Data.Entity.Validation;
using WebMatrix.WebData;

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
        // GET: /Payment/Create

        public ActionResult Create(string ReqNo)
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewData["Company"] = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();
            ViewData["CompanyID"] = comid;  

            int Req = Convert.ToInt16(ReqNo);
            decimal PassTot = PaymentFacade.GetPassTotal(Req);
            ViewBag.Total = PassTot;
            ViewBag.ReqNo = ReqNo;

            var payTypes = new List<string> { "Cash" };
            ViewBag.payTypes = new SelectList(payTypes);
            
            return View();
        }

        //
        // POST: /Payment/Create

        [HttpPost]
        public ActionResult Create(PaymentDetail paymentdetail, int reqno, decimal price)
        {
            try
            {
                int Ref = 0;
                
                // Get relevent request details
                PassRequestHed passReq = new PassRequestHed();
                passReq = db.PassRequestHeds.Find(reqno);                

                PassIssueController PIC = new PassIssueController();
                using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    Ref = PIC.SavePassIssueHed(PassIssueFacade.MapModelToHed(passReq));
                    if (Ref > 0)
                    {
                        PIC.SavePassIssueDet(PassIssueFacade.MapModelToDet(passReq), Ref);
                        PIC.SavePassIssueVehi(PassIssueFacade.MapModelToVehi(passReq), Ref);

                        // Save Payment details
                        paymentdetail.PassNo = Ref;
                        paymentdetail.PassTotal = price;
                        paymentdetail.AddDate = DateTime.Now;
                        paymentdetail.AddUser = WebSecurity.CurrentUserName;
                        db.PaymentDetails.Add(paymentdetail);

                        // Update Pass request (Paid & Issued flags)
                        passReq = PassIssueFacade.UpdatePassReq(passReq);
                        db.Entry(passReq).State = System.Data.EntityState.Modified;
                        db.SaveChanges();     

                        Scope.Complete();
                    }
                }           

                return RedirectToAction("Complete");
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult Client(string ReqNo)
        {
            var comid = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(s => s.CompanyID).First();
            ViewData["Company"] = db.Companies.Where(c => c.CompanyID == comid).Select(n => n.CompanyName).First().ToString();
            ViewData["CompanyID"] = comid;  

            int Req = Convert.ToInt16(ReqNo);
            decimal PassTot = PaymentFacade.GetPassTotal(Req);
            ViewBag.Total = PassTot;
            ViewBag.ReqNo = ReqNo;

            return View();
        }

        /// <summary>
        /// Pays the pal.
        /// </summary>
        /// <returns></returns>
        public string SendSms(string SmsId)
        {
            // SMS Logic
            var client = new TwilioRestClient("AC73a7e5cc8ceb71599d77a664307425ee", "58f1829a5b6534d6acd3a25041742d17");
            var result = client.SendSmsMessage("+13312085208", "+94777006211", "From Biyagama EPZ! Your Pass No : " + SmsId);
            string Sms = "";

            if (result.Status.ToString() != "Failed")
            {
                Sms = "SMS sent successfully. It will be delivered shortly.";
            }
            else
            {
                Sms = "SMS sending failed. Please try again later.";
            }

            return Sms;
        }

        public ActionResult Complete()
        {
            return View();
        }
    }
}
