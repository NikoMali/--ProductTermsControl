using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class ResponsiblePersonsGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //refferecee
        public List<ProductToBranch> ProductToBranchs { get; set; }
    }
}
