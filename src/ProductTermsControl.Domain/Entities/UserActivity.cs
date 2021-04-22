using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class UserActivity: BaseEntity
    {
        public string Data { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string response { get; set; }
        public string ActivityType { get; set; }
    }
}
