namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class CardDetail
    {
        [Key]
        public string PaymentNo { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string NameOnCard { get; set; }
        public System.DateTime AddDate { get; set; }
        public string AddUser { get; set; }
    
        public virtual PaymentDetail PaymentDetail { get; set; }
    }
}
