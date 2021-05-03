using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductTermsControl.Domain.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
        public bool IsActive { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}