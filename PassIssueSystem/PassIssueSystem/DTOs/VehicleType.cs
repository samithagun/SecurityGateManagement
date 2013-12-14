namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class VehicleType
    {
        public VehicleType()
        {
            this.PassReqVehicles = new HashSet<PassReqVehicle>();
        }

        [Key]
        [Required]
        [StringLength(8)]
        [Display(Name = "Vehicle Code")]
        public string VehicleCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Fee")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal VehicleFee { get; set; }

        [Display(Name = "Status")]
        public decimal Status { get; set; }

        [Display(Name = "Added Date")]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Added By")]
        public string AddUser { get; set; }

        public virtual ICollection<PassReqVehicle> PassReqVehicles { get; set; }
    }
}
