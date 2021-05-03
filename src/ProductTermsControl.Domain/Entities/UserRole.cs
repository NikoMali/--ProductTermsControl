using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTermsControl.Domain.Entities
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        //referencee
        public int RoleId { get; set; }
        public Role Roles { get; set; }

        public string OtherPermissions { get; set; }
    }
}