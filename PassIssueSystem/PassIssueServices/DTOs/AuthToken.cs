using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassIssueServices.DTOs
{
    [DataContract(Namespace = "PassIssueServices", Name = "AuthToken")]
    [Serializable]

    public class AuthToken
    {
        [DataMember(Order = 0)]
        public string UserID { get; set; }

        [DataMember(Order = 1)]
        public string SessionData { get; set; }
    }
}