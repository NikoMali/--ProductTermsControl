using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class MagazineBranch
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }



        //referencee
        public int MagazineId { get; set; }
        public Magazine Magazine { get; set; }

        public List<ProductToBranch> ProductToBranchs { get; set; }
        public List<UserReference> UserReferences { get; set; }

        public MagazineBranch() { }

        public MagazineBranch(MagazineBranch magazineBranch, Magazine magazine)
        {
            Id = magazineBranch.Id;
            IdentificationCode = magazineBranch.IdentificationCode;
            CreateDate = magazineBranch.CreateDate;
            UpdateDate = magazineBranch.UpdateDate;
            Name = magazineBranch.Name;
            Location = magazineBranch.Location;
            MagazineId = magazine.Id;
            Magazine = magazine;
        }
    }
}
