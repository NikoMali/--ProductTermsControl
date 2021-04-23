using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.HelperModel
{
    public class UserActivityByDate
    {
        public DateTime Date { get; set; }
        public List<UserActivity> Activities { get; set; }

        public UserActivityByDate() { }

        public UserActivityByDate(List<UserActivity> userActivities)
        {
            Date = userActivities[0].CreateDate.Date;
            Activities = userActivities;
        }
    }
    
}
