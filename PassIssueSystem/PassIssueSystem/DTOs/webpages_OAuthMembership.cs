namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class webpages_OAuthMembership
    {
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
        public int UserId { get; set; }
    }
}
