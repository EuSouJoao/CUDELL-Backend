using System;
using System.Collections.Generic;

namespace WEBAPI.Models
{
    public partial class Audit
    {
        public int AuditId { get; set; }
        public DateTime OperationTimeStamp { get; set; }
        public string Operation { get; set; }
        public string Username { get; set; }
    }
}
