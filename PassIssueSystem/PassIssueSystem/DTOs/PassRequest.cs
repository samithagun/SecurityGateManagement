namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;

    public partial class PassRequestHed
    {
        [Key]
        [Display(Name = "Pass Request No")]
        public int PassReqNo { get; set; }

        [Required]
        [Display(Name = "Company ID")]
        public string CompanyID { get; set; }

        [Required]
        [Display(Name = "Required From")]
        [DataType(DataType.Date)]
        public System.DateTime RequiredFrom { get; set; }

        [Required]
        [Display(Name = "Required To")]
        [DataType(DataType.Date)]
        public System.DateTime RequiredTo { get; set; }

        [Required]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Added Date")]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Added By")]
        public string AddUser { get; set; }

        [Display(Name = "Pass Issued")]
        public bool Issued { get; set; }

        [Display(Name = "Payment Done")]
        public bool Paid { get; set; }

        public virtual Company Company { get; set; }
        public virtual List<PassRequestDet> PassRequestDets { get; set; }
        public virtual List<PassReqVehicle> PassReqVehicles { get; set; }
        public virtual ICollection<PassIssueHed> PassIssueHeds { get; set; }
        public virtual ICollection<CheckInPerson> CheckInPersons { get; set; }
    }

    public partial class PassRequestDet
    {
        [Key]
        [Display(Name = "Pass Request No")]
        public int PassReqNo { get; set; }

        [Required]
        [Display(Name = "NIC No")]
        [StringLength(10, MinimumLength = 9)]
        public string PersonNIC { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string PersonName { get; set; }

        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 9)]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Pass Type")]
        public string PassCode { get; set; }

        [Display(Name = "Pass Fee")]
        public decimal PassFee { get; set; }

        public virtual PassRequestHed PassRequestHed { get; set; }
        public virtual PassType PassType { get; set; }
    }

    public partial class PassReqVehicle
    {
        [Key]
        [Display(Name = "Pass Request No")]
        public int PassReqNo { get; set; }

        [Display(Name = "Vehicle Type")]
        public string VehicleCode { get; set; }

        [Display(Name = "Vehicle No")]
        [StringLength(12, MinimumLength = 6)]
        public string VehicleNo { get; set; }

        [Display(Name = "Vehicle Fee")]
        public decimal VehicleFee { get; set; }

        public virtual PassRequestHed PassRequestHed { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}