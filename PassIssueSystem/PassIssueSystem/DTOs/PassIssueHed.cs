namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PassIssueHed
    {
        [Key]
        public string PassNo { get; set; }

        [Required]
        [Display(Name = "Pass Request No")]
        public int PassReqNo { get; set; }

        [Display(Name = "Issued Date")]
        public System.DateTime IssueDate { get; set; }

        [Required]
        [Display(Name = "Valid From")]
        public System.DateTime ValidFrom { get; set; }

        [Required]
        [Display(Name = "Valid To")]
        public System.DateTime ValidTo { get; set; }

        [Display(Name = "VIP")]
        public decimal IsVIP { get; set; }

        public System.DateTime AddDate { get; set; }
        public string AddUser { get; set; }
    
        public virtual PassRequestHed PassRequestHed { get; set; }
        public virtual ICollection<PassIssueDet> PassIssueDets { get; set; }
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
        public virtual ICollection<CheckInPerson> CheckInPersons { get; set; }
        public virtual ICollection<CheckInVehicle> CheckInVehicles { get; set; }
        public virtual ICollection<PassIssueVehicle> PassIssueVehicles { get; set; }
    }
}
