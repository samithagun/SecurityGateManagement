namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class CheckInVehicle
    {
        [Key]
        public string PassNo { get; set; }
        public string VehicleNo { get; set; }
        public System.DateTime InTime { get; set; }
        public System.DateTime OutTime { get; set; }

        public virtual PassIssueDet PassIssueDet { get; set; }
        public virtual PassIssueHed PassIssueHed { get; set; }
    }
}
