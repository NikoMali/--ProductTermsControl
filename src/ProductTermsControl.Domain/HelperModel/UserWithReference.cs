using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.HelperModel
{
    public class UserWithReference    {
        public User user { get; set; }
        public UserReference userReference { get; set; }
    }
}
