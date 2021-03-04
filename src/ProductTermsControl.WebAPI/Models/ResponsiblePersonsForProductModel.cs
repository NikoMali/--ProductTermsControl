using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.WebAPI.Models
{ 
    public class ResponsiblePersonsForProductModel
    {
        public int Id { get; set; }
        public int ResponsiblePersonsGroupId { get; set; }
        public int UserId { get; set; }

    }
}
