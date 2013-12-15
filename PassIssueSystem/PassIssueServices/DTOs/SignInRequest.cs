using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassIssueServices.DTOs
{
    [DataContract(Namespace = "PassIssueServices", Name = "SignInRequest")]
    [Serializable]

    public class SignInRequest
    {
        [DataMember(Order = 0)]
        public string UserName { get; set; }

        [DataMember(Order = 1)]
        public string Password { get; set; }
    }
}