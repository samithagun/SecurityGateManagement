using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassIssueServices.DTOs
{
    [DataContract(Namespace = "PassIssueServices", Name = "PassRequest")]
    [Serializable]

    public class PassRequest
    {
        [DataMember(Order = 0)]
        public string PassNo { get; set; }
    }
}