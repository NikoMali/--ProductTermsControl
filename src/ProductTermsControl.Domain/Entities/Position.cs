using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class Position : BaseEntity
    {
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        



        //referencee
        public List<UserReference> UserReferences { get; set; }
    }
}
