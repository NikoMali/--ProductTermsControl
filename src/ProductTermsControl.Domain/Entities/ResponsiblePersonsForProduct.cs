using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class ResponsiblePersonsForProduct : BaseEntity
    {
       
        public DateTime RegisterDate { get; set; }

        //referencee
        public int ResponsiblePersonsGroupId { get; set; }
        public ResponsiblePersonsGroup ResponsiblePersonsGroup { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ResponsiblePersonsForProduct() { }
        public ResponsiblePersonsForProduct(ResponsiblePersonsForProduct responsiblePersonsForProduct, ResponsiblePersonsGroup responsiblePersonsGroup, User user)
        {
            Id = responsiblePersonsForProduct.Id;
            RegisterDate = responsiblePersonsForProduct.RegisterDate;
            ResponsiblePersonsGroupId = responsiblePersonsForProduct.ResponsiblePersonsGroupId;
            ResponsiblePersonsGroup = responsiblePersonsGroup;
            UserId = responsiblePersonsForProduct.UserId;
            User = user;
        }

        public void Create(ResponsiblePersonsForProduct responsiblePersonsForProduct)
        {
            responsiblePersonsForProduct.RegisterDate = DateTime.Now;
        }


    }
}
