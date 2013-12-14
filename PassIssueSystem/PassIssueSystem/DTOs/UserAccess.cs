namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class UserAccess
    {
        [Key]
        public string ModuleID { get; set; }
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public bool Authorize { get; set; }
    }
}
