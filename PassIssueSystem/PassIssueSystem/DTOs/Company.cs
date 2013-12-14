namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Company
    {
        public Company()
        {
            this.PassRequestHeds = new HashSet<PassRequestHed>();
        }

        [Key]
        [Required]
        [Display(Name = "Company ID")]
        [StringLength(15, MinimumLength = 2)]
        public string CompanyID { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "Address 3")]
        public string Address3 { get; set; }

        [Required]
        [Display(Name = "BOI Approval No")]
        public string BOIApproveNo { get; set; }

        [Required]
        [Display(Name = "BOI Approved Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime BOIApproveDate { get; set; }

        [Display(Name = "Telephone 1")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone1 { get; set; }

        [Display(Name = "Telephone 2")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone2 { get; set; }

        [Display(Name = "Fax")]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [Display(Name = "Status")]
        public decimal Status { get; set; }

        [Display(Name = "Added Date")]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Added By")]
        public string AddUser { get; set; }

        public virtual ICollection<PassRequestHed> PassRequestHeds { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
