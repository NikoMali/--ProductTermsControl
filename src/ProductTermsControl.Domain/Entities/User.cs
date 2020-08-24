using System;
using System.Collections.Generic;

namespace ProductTermsControl.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }




        //referencee
        public int MagazineBranchId { get; set; }
        public MagazineBranch MagazineBranchs { get; set; }

        public List<ResponsiblePersonsForProduct> ResponsiblePersonsByProducts { get; set; }
    }
}