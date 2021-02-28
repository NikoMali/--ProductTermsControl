using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Helpers
{
    internal class SortProductToBrunchWithCheckTerm : IComparer<ProductWithTerm>
    {
        public string Name { get; set; }
        public int Salary { get; set; }

        //SortBySalaryByAscendingOrder
        public int Compare(ProductWithTerm x,ProductWithTerm y)
        {
            if (
                x.ProductToBranch.TermDate.AddDays(-x.ProductToBranch.DaysBeforeNotifiWarning) 
                <=
                y.ProductToBranch.TermDate.AddDays(-y.ProductToBranch.DaysBeforeNotifiWarning)
                ) return 1;

            else if (
                x.ProductToBranch.TermDate.AddDays(-x.ProductToBranch.DaysBeforeNotifiWarning)
                >
                y.ProductToBranch.TermDate.AddDays(-y.ProductToBranch.DaysBeforeNotifiWarning)
                ) return -1;

            else return 0;
        }

        //SortBySalaryByDescendingOrder
        //public int Compare(Employee x, Employee y)
        //{
        //    if (x.Salary < y.Salary) return 1;
        //    else if (x.Salary > y.Salary) return -1;
        //    else return 0;
        //}
    }
}
