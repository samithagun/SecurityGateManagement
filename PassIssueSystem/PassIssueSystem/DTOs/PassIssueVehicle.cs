namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PassIssueVehicle
    {
        [Key]
        public int PassNo { get; set; }
        public string VehicleNo { get; set; }
    
        public virtual CheckInVehicle CheckInVehicle { get; set; }
        public virtual PassIssueHed PassIssueHed { get; set; }
    }
}
