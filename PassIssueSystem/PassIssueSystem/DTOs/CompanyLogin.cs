namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class CompanyLogin
    {
        [Key]
        public string CompanyID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Status { get; set; }
        public System.DateTime AddDate { get; set; }
        public string AddUser { get; set; }
    }
}
