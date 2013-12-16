namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PaymentDetail
    {
        [Key]
        public int PaymentNo { get; set; }
        public int PassNo { get; set; }
        public decimal PassTotal { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public System.DateTime AddDate { get; set; }
        public string AddUser { get; set; }

        public virtual ICollection<CardDetail> CardDetails { get; set; }
        public virtual PassIssueHed PassIssueHed { get; set; }
        public virtual CardDetail CardDetail { get; set; }
    }
}
