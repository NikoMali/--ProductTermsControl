using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTermsControl.WebAPI.Models
{
    public class ProductWithTermModel
    {
        public ProductModel Product { get; set; }
        public ProductToBranchModel ProductToBranch { get; set; }
    }
}
