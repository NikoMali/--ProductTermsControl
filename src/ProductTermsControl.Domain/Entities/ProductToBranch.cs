﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class ProductToBranch
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime TermDate { get; set; }
        public int DaysBeforeNotifiWarning { get; set; }
        public int Quantity { get; set; }



        //referencee
        public int ProductId { get; set; }
        public  Product Product { get; set; }
        public int ResponsiblePersonsGroupId { get; set; }
        public ResponsiblePersonsGroup ResponsiblePersonsGroup { get; set; }
        public int MagazineBranchId { get; set; }
        public MagazineBranch MagazineBranch { get; set; }

        public ProductToBranch() { }
        public ProductToBranch(ProductToBranch productToBranch, Product product, MagazineBranch magazineBranch, ResponsiblePersonsGroup responsiblePersonsGroup)
        {
            Id = productToBranch.Id;
            RegisterDate = productToBranch.RegisterDate;
            TermDate = productToBranch.TermDate;
            DaysBeforeNotifiWarning = productToBranch.DaysBeforeNotifiWarning;
            Quantity = productToBranch.Quantity;
            ProductId = productToBranch.ProductId;
            Product = product;
            MagazineBranchId = productToBranch.MagazineBranchId;
            MagazineBranch = magazineBranch;
            ResponsiblePersonsGroupId = productToBranch.ResponsiblePersonsGroupId;
            ResponsiblePersonsGroup = responsiblePersonsGroup;

        }
    }
}
