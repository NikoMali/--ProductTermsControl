using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class UserActivity: BaseEntity
    {
        public string RequestData { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string IpAddress { get; set; }
        [Column(TypeName = "LONGTEXT")]
        public string response { get; set; }
        public string ActivityType { get; set; }
    }
}
