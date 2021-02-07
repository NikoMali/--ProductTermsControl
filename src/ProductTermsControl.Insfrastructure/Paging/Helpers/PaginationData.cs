using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductTermsControl.Insfrastructure.Paging.Helpers
{
    public class PaginationData
    {
        public static GetAllWithPaging<T> GetAllForPaging<T>(int PageNumber, int PageSize, List<T> data)
        {

            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var pagedData = data
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();


            var totalRecords = data.Count();
            var result = new GetAllWithPaging<T>(validFilter, pagedData, totalRecords);
            return result;
        }
    }
}
