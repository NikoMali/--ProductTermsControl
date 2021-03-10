using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class BranchProductStock : BaseEntity
    {
        
        public bool IsOutOfStock { get; set; }
        public string OutOfStockReason { get; set; }
        

        //referencee
        public int ProductToBranchId { get; set; }
        public ProductToBranch ProductToBranch { get; set; }


        public BranchProductStock() { }
        public BranchProductStock(BranchProductStock branchProductStock, ProductToBranch productToBranch)
        {
            Id = branchProductStock.Id;
            IsOutOfStock = branchProductStock.IsOutOfStock;
            OutOfStockReason = branchProductStock.OutOfStockReason;
            ProductToBranchId = branchProductStock.ProductToBranchId;
            ProductToBranch = productToBranch;
            
        }
        public void AddItem(BranchProductStock branchProductStock)
        {
            Id = branchProductStock.Id;
            IsOutOfStock = branchProductStock.IsOutOfStock;
            OutOfStockReason = branchProductStock.OutOfStockReason;
            ProductToBranchId = branchProductStock.ProductToBranchId;
            
        }
        public void UpdateItem(BranchProductStock branchProductStock)
        {
            Id = branchProductStock.Id;
            IsOutOfStock = branchProductStock.IsOutOfStock;
            OutOfStockReason = branchProductStock.OutOfStockReason;
            ProductToBranchId = branchProductStock.ProductToBranchId;
            
        }
    }
}
