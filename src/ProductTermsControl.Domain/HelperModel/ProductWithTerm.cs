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
    }
}
