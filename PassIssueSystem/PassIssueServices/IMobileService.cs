using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PassIssueServices.DTOs;

namespace PassIssueServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMobileService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SignInUser")]
        SignInResponse SignInUser(SignInRequest Request);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Xml,
        RequestFormat = WebMessageFormat.Xml,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "CheckPass")]
        PassResponse CheckPass(PassRequest Request);
    }
}
