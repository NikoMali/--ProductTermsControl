using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class Magazine : BaseEntity
    {
       
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
       



        //referencee
        public List<MagazineBranch> MagazineBranchs { get; set; }
    }
}
