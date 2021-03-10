using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTermsControl.WebAPI.Models
{
    public class BranchProductStockResponseModel
    {
        public int Id { get; protected set; }
        public bool IsOutOfStock { get; set; }
        public string OutOfStockReason { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //referencee
        public int ProductToBranchId { get; set; }
        public ProductToBranchModel ProductToBranch { get; set; }
    }
}
