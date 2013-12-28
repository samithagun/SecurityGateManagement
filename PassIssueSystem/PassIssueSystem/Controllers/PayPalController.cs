using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PassIssueSystem.Controllers;
using PassIssueSystem.Facades;
using PassIssueSystem.Models;
using PassIssueSystem.Twilio;
using PayPal.Api.Payments;
using PayPal.Exception;
using PayPal.Manager;
using PayPal.Models;

namespace PayPal.Controllers
{
    public class PayPalController : Controller
    {
        private Entities db = new Entities();
        public string smsId;

        //
        // GET: /Payment/

        public ActionResult CreatePayment(string description, decimal price, decimal tax = 0, decimal shipping = 0)
        {
            var viewData = new PayPalViewData();
            var guid = Guid.NewGuid().ToString();            
            
            // Newly added. Keeps the Request No as in a global variable
            HttpContext.Application["smsId"] = description;

            var paymentInit = new Payment
            {
                intent = "authorize",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = "USD",
                            total = (price + tax + shipping).ToString(),
                            details = new Details
                            {
                                subtotal = price.ToString(),
                                tax = tax.ToString(),
                                shipping = shipping.ToString()
                            }
                        },
                        description = description
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = Utilities.ToAbsoluteUrl(HttpContext, String.Format("~/paypal/confirmed?id={0}", guid)),
                    cancel_url = Utilities.ToAbsoluteUrl(HttpContext, String.Format("~/paypal/canceled?id={0}", guid)),
                },
            };

            viewData.JsonRequest = JObject.Parse(paymentInit.ConvertToJson()).ToString(Formatting.Indented);

            try
            {
                var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var createdPayment = paymentInit.Create(apiContext);                

                var approvalUrl = createdPayment.links.ToArray().FirstOrDefault(f => f.rel.Contains("approval_url"));

                if (approvalUrl != null)
                {
                    Session.Add(guid, createdPayment.id);

                    return Redirect(approvalUrl.href);
                }

                viewData.JsonResponse = JObject.Parse(createdPayment.ConvertToJson()).ToString(Formatting.Indented);

                return View("Error", viewData);
            }
            catch (PayPalException ex)
            {
                viewData.ErrorMessage = ex.Message;

                return View("Error", viewData);
            }
        }

        /// <summary>
        /// Confirmeds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <param name="payerId">The payer identifier.</param>
        /// <returns></returns>
        public ActionResult Confirmed(Guid id, string token, string payerId)
        {
            var viewData = new ConfirmedViewData
            {
                Id = id,
                Token = token,
                PayerId = payerId
            };

            var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            var payment = new Payment()
            {
                id = (string)Session[id.ToString()],
            };

            var executedPayment = payment.Execute(apiContext, new PaymentExecution { payer_id = payerId });

            viewData.AuthorizationId = executedPayment.transactions[0].related_resources[0].authorization.id;
            viewData.JsonRequest = JObject.Parse(payment.ConvertToJson()).ToString(Formatting.Indented);
            viewData.JsonResponse = JObject.Parse(executedPayment.ConvertToJson()).ToString(Formatting.Indented);

            return View(viewData);
        }

        /// <summary>
        /// Canceleds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <param name="payerId">The payer identifier.</param>
        /// <returns></returns>
        public ActionResult Canceled(Guid id, string token, string payerId)
        {
            return Content("Let's Try Again!");
        }

        /// <summary>
        /// Captures the specified authorization identifier.
        /// </summary>
        /// <param name="authorizationId">The authorization identifier.</param>
        /// <returns></returns>
        public ActionResult Capture(string authorizationId)
        {
            var viewData = new PayPalViewData();

            try
            {
                var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var authorization = Authorization.Get(apiContext, authorizationId);

                if (authorization != null)
                {
                    var total = Convert.ToDecimal(authorization.amount.total);

                    var capture = authorization.Capture(apiContext, new Capture
                       {
                           is_final_capture = true,
                           amount = new Amount
                           {
                               currency = "USD",
                               total = (total + (total * .05m)).ToString("f2")
                           },
                       });

                    viewData.JsonResponse = JObject.Parse(capture.ConvertToJson()).ToString(Formatting.Indented);
                    
                    // Newly added
                    // Sends the Sms
                    var smsId = HttpContext.Application["smsId"].ToString();
                    PaymentController PC = new PaymentController();
                    TempData["SMS"] = PC.SendSms(smsId);

                    // Get relevent request details
                    PassRequestHed passReq = new PassRequestHed();
                    passReq = db.PassRequestHeds.Find(Convert.ToInt16(smsId));     

                    // Update Pass request (Paid & Issued flags)
                    passReq = PassIssueFacade.UpdatePassReq(passReq);
                    db.Entry(passReq).State = System.Data.EntityState.Modified;
                    db.SaveChanges();     

                    return View("Success");
                }

                viewData.ErrorMessage = "Could not find previous authorization.";

                return View("Error", viewData);
            }
            catch (PayPalException ex)
            {
                viewData.ErrorMessage = ex.Message;

                return View("Error", viewData);
            }
        }

        public ActionResult Void(string authorizationId)
        {
            var viewData = new PayPalViewData();

            try
            {
                var accessToken = new OAuthTokenCredential(ConfigManager.Instance.GetProperties()["ClientID"], ConfigManager.Instance.GetProperties()["ClientSecret"]).GetAccessToken();
                var apiContext = new APIContext(accessToken);
                var authorization = Authorization.Get(apiContext, authorizationId);

                if (authorization != null)
                {
                    var voidedAuthorization = authorization.Void(apiContext);

                    viewData.JsonResponse = JObject.Parse(voidedAuthorization.ConvertToJson()).ToString(Formatting.Indented);

                    return View(viewData);
                }

                viewData.ErrorMessage = "Could not find previous authorization.";

                return View("Error", viewData);
            }
            catch (PayPalException ex)
            {
                viewData.ErrorMessage = ex.Message;

                return View("Error", viewData);
            }
        }
    }
}
