using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassIssueSystem.Models
{   
    public partial class PassRequestHed
    {
        [Key]
        [Display(Name = "Pass Request No")]
        public string PassReqNo { get; set; }

        [Required]
        [Display(Name = "Company ID")]
        public string CompanyID { get; set; }

        [Required]
        [Display(Name = "Required From")]
        public System.DateTime RequiredFrom { get; set; }

        [Required]
        [Display(Name = "Required To")]
        public System.DateTime RequiredTo { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        public System.DateTime AddDate { get; set; }
        public string AddUser { get; set; }

        public virtual Company Company { get; set; }
        public virtual List<PassRequestDet> PassRequestDets { get; set; }
        public virtual List<PassReqVehicle> PassReqVehicles { get; set; }
    }

    public partial class PassRequestDet
    {
        [Key]
        public string PassReqNo { get; set; }
        public string PersonNIC { get; set; }
        public string PersonName { get; set; }
        public string PassCode { get; set; }
        public decimal PassFee { get; set; }

        public virtual PassType PassType { get; set; }
    }

    public partial class PassReqVehicle
    {
        [Key]
        public string PassReqNo { get; set; }
        public string VehicleCode { get; set; }
        public string VehicleNo { get; set; }
        public decimal VehicleFee { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}