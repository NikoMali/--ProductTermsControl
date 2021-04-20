using ProductTermsControl.Domain.Entities;
using ProductTermsControl.WebAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.WebAPI.Models
{
    public class SectionWithUsersAndProductsModel
    {
        public ResponsiblePersonsGroupModel Section { get; set; }
        public List<UserModel> users { get; set; }
        public List<ProductResponseModel> products { get; set; }


        
    }
}
