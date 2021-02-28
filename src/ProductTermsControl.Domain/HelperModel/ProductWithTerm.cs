using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.HelperModel
{
    public class ProductWithTerm
    {
        public Product Product { get; set; }
        public ProductToBranch ProductToBranch { get; set; }
        public bool IsWarning { get; set; } = false;
        public DateTime WarningTermDateBegin { get; set; }

        public ProductWithTerm() { }

        public ProductWithTerm(Product product, ProductToBranch productToBranch)
        {
            WarningTermDateBegin = productToBranch.TermDate.AddDays(-productToBranch.DaysBeforeNotifiWarning);
            if (DateTime.Now >= WarningTermDateBegin)
            {
                IsWarning = true;
            }
            Product = product;
            ProductToBranch = productToBranch; 
        }


    }
}
