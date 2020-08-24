using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class Magazine
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }



        //referencee
        public List<MagazineBranch> MagazineBranchs { get; set; }
    }
}
