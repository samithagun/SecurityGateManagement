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
using System.Web.Security;
using PassIssueSystem.Filters;

namespace PassIssueServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class MobileService : IMobileService
    {
        string password = "help";
        public SignInResponse SignInUser(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: false); 
                }

                if (WebSecurity.Login(request.UserName, request.Password))
                {
                    response.isSuccess = true;
                    response.AuthenticationToken = new AuthToken();
                    MembershipUser currentUser = Membership.GetUser(request.UserName, true /* userIsOnline */);
                    string tokenValue = currentUser.UserName + "-" + currentUser.Email;
                    AuthToken authT = new AuthToken();
                    authT.UserID = currentUser.Email;
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
    }
}
