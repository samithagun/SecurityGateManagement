using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using PassIssueServices.DTOs;
using WebMatrix.WebData;
//using System.Web.Security;
using PassIssueSystem.Filters;
using System.ServiceModel.Activation;
using PassIssueSystem.Models;

namespace PassIssueServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]

    public class MobileService : IMobileService
    {
        string password = "zILL#940";

        public SignInResponse SignInUser(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                // This method needs to be called before using any other method relating to Entity Framework.
                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: false);
                }

                if (WebSecurity.Login(request.UserName, request.Password, persistCookie: false))
                {
                    response.isSuccess = true;
                    response.AuthenticationToken = new AuthToken();
                    MembershipUser currentUser = Membership.GetUser(request.UserName, true /* userIsOnline */);
                    string tokenValue = currentUser.UserName;

                    AuthToken authT = new AuthToken();
                    authT.UserID = WebSecurity.GetUserId(request.UserName).ToString();
                    authT.SessionData = StringCipher.Encrypt(tokenValue, password);

                    response.AuthenticationToken = authT;
                }
                else
                {
                    response.isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.AuthenticationToken.SessionData = ex.Message;
            }

            return response;
        }

        public PassResponse CheckPass(PassRequest request)
        {
            PassResponse response = new PassResponse();

            try
            {
                Entities db = new Entities();
                PassRequestHed PRH;

                int ReqNo = Convert.ToInt16(request.PassNo);

                // Get pass request details
                PRH = db.PassRequestHeds.ToList().Where(p => p.PassReqNo == ReqNo && p.Issued == true).FirstOrDefault();

                // Check whether there are any valid data
                if (PRH.PassReqNo != 0)
                {
                    response.isValid = true;
                    response.AuthenticationToken = new AuthToken();
                    string tokenValue = PRH.CompanyID;

                    response.personName = db.PassRequestDets.Where(i => i.PassReqNo == PRH.PassReqNo).Select(d => d.PersonName).First().ToString();
                    response.personNIC = db.PassRequestDets.Where(i => i.PassReqNo == PRH.PassReqNo).Select(d => d.PersonNIC).First().ToString();
                    response.issuedCompany = db.Companies.Where(i => i.CompanyID == PRH.CompanyID).Select(d => d.CompanyName).First().ToString();
                    response.validFrom = PRH.RequiredFrom;
                    response.validTo = PRH.RequiredTo;

                    AuthToken authT = new AuthToken();
                    authT.UserID = PRH.AddUser;
                    authT.SessionData = StringCipher.Encrypt(tokenValue, password);

                    response.AuthenticationToken = authT;
                }
                else
                {
                    response.isValid = false;
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.AuthenticationToken.SessionData = ex.Message;
            }

            return response;
        }
    }
}
