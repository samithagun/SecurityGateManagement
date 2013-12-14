namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class webpages_Roles
    {    
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
