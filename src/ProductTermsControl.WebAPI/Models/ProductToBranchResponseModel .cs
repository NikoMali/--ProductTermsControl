using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.WebAPI.Models
{
    public class ProductToBranchResponseModel
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime TermDate { get; set; }
        public int DaysBeforeNotifiWarning { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int MagazineBranchId { get; set; }
        public MagazineBranchModel MagazineBranch { get; set; }
        public int ResponsiblePersonsGroupId { get; set; }
        public ResponsiblePersonsGroupModel ResponsiblePersonsGroup { get; set; }
    }
}
