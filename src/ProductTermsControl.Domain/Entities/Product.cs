using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentificationCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //referencee
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<ProductToBranch> ProductToBranchs { get; set; }

        public Product() { }
        public Product(Product product, Company company)
        {
            Id = product.Id;
            Name = product.Name;
            IdentificationCode = product.IdentificationCode;
            CreateDate = product.CreateDate;
            UpdateDate = product.UpdateDate;
            CompanyId = company.Id;
            Company = company;
        }
    }
}
