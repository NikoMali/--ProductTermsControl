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
        public int? MagazineBranchId { get; set; }
        public MagazineBranch MagazineBranchs { get; set; }

        public int? PositionId { get; set; }
        public Position Positions { get; set; }

        public UserReference() { }
        public UserReference(UserReference userReference, User user,MagazineBranch magazineBranch ,Position position)
        {
            UserId = userReference == null ? 0 : userReference.UserId;
            User = user;

            MagazineBranchId = userReference == null ? 0 : userReference.MagazineBranchId;
            MagazineBranchs = magazineBranch;

            PositionId = userReference == null ? 0 : userReference.PositionId;
            Positions = position;
        }
    }
}