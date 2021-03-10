using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class ResponsiblePersonsGroup : BaseEntity
    {
        
        public string Name { get; set; }
        

        //refferecee
        public List<ProductToBranch> ProductToBranchs { get; set; }
    }
}
