using ProductTermsControl.WebAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTermsControl.WebAPI.Models
{
    public class UserActivityModel
    {
        public string RequestData { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public string response { get; set; }
        public string ActivityType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
