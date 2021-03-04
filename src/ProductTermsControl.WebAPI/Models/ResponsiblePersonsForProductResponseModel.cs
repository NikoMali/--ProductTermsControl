using ProductTermsControl.Domain.Entities;
using ProductTermsControl.WebAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.WebAPI.Models
{ 
    public class ResponsiblePersonsForProductResponseModel
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public int ResponsiblePersonsGroupId { get; set; }
        public ResponsiblePersonsGroupModel ResponsiblePersonsGroup { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }

    }
}
