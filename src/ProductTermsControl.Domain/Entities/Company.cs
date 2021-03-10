using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class Company : BaseEntity
    {
       
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
       
    }
}
