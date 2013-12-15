using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassIssueServices.DTOs
{
    [DataContract(Namespace = "PassIssueServices", Name = "SignInResponse")]
    [Serializable]

    public class SignInResponse
    {
        [DataMember(Order = 0)]
        public bool isValid { get; set; }

        [DataMember(Order = 1)]
        public AuthToken AuthToken { get; set; }
    }
}