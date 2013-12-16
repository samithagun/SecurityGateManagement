namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PassIssueDet
    {
        [Key]
        public int PassNo { get; set; }
        public string PersonNIC { get; set; }
        public string MobileNo { get; set; }
    
        public virtual PassIssueHed PassIssueHed { get; set; }
    }
}
