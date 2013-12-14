namespace PassIssueSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class C__MigrationHistory
    {
        public string MigrationId { get; set; }
        public byte[] Model { get; set; }
        public string ProductVersion { get; set; }
    }
}
