using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class ReasonForOutOfStock : BaseEntity
    {
        
        public string Name { get; set; }
        public bool IsActive { get; set; }
        



        //referencee
        public List<BranchProductStock> BranchProductStocks { get; set; }
    }
}
