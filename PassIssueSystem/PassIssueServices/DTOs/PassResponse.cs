using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PassIssueServices.DTOs
{
    [DataContract(Namespace = "PassIssueServices", Name = "PassResponse")]
    [Serializable]

    public class PassResponse
    {
        [DataMember(Order = 0)]
        public bool isValid { get; set; }

        [DataMember(Order = 1)]
        public string personName { get; set; }

        [DataMember(Order = 2)]
        public string personNIC { get; set; }

        [DataMember(Order = 3)]
        public string issuedCompany { get; set; }

        [DataMember(Order = 4)]
        public DateTime validFrom { get; set; }

        [DataMember(Order = 5)]
        public DateTime validTo { get; set; }
        
        [DataMember(Order = 6)]
        public AuthToken AuthenticationToken { get; set; }
    }
}