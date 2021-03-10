using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductTermsControl.WebAPI.Models
{
    public class BranchProductStockModel
    {
        public int Id { get; set; }
        public bool IsOutOfStock { get; set; }
        public string OutOfStockReason { get; set; }
        public int ProductToBranchId { get; set; }
    }
}
