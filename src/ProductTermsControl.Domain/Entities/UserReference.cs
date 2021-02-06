using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTermsControl.Domain.Entities
{
    public class UserReference
    {
        [Key]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        //referencee
        public int MagazineBranchId { get; set; }
        public MagazineBranch MagazineBranchs { get; set; }


    }
}