namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PassType
    {
        public PassType()
        {
            this.PassRequestDets = new HashSet<PassRequestDet>();
        }

        [Key]
        [Required]
        [StringLength(8)]
        [Display(Name = "Pass Type Code")]
        public string PassCode { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Fee")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal PassFee { get; set; }

        [Display(Name = "Status")]
        public decimal Status { get; set; }

        [Display(Name = "Added Date")]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Added By")]
        public string AddUser { get; set; }

        public virtual ICollection<PassRequestDet> PassRequestDets { get; set; }
    }
}