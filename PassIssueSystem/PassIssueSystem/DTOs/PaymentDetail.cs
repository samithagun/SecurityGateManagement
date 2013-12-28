namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PaymentDetail
    {
        [Key]
        [Required]
        [Display(Name = "Payment No")]
        public int PaymentNo { get; set; }

        [Required]
        [Display(Name = "Company ID")]
        public int PassNo { get; set; }

        [Required]
        [Display(Name = "Total Amount")]
        public decimal PassTotal { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        public System.DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Added Date")]
        public System.DateTime AddDate { get; set; }

        [Display(Name = "Added By")]
        public string AddUser { get; set; }

        public virtual ICollection<CardDetail> CardDetails { get; set; }
        public virtual PassIssueHed PassIssueHed { get; set; }
        public virtual CardDetail CardDetail { get; set; }
    }
}
