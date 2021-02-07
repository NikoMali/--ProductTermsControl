using ProductTermsControl.Insfrastructure.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Insfrastructure.Helpers
{
    public class GetAllWithPaging<T>
    {
        public PaginationFilter PaginationFilter { get; set; }
        public List<T> entities { get; set; }
        public int totalRecords { get; set; }
        public GetAllWithPaging(PaginationFilter PaginationFilter, List<T> entities, int totalRecords)
        {
            this.PaginationFilter = PaginationFilter;
            this.entities = entities;
            this.totalRecords = totalRecords;
        }
    }
}
