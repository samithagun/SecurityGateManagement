namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class CheckInPerson
    {
        [Key]
        public int PassNo { get; set; }
        public string PersonNIC { get; set; }
        public System.DateTime InTime { get; set; }
        public System.DateTime OutTime { get; set; }
    
        public virtual PassIssueDet PassIssueDet { get; set; }
        public virtual PassIssueHed PassIssueHed { get; set; }
    }
}
