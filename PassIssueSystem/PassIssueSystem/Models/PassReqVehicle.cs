//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PassReqVehicle
    {
        [Key]
        public string PassReqNo { get; set; }
        public string VehicleCode { get; set; }
        public string VehicleNo { get; set; }
        public decimal VehicleFee { get; set; }
    
        public virtual PassRequestHed PassRequestHed { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}