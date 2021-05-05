using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.Entities
{
    public class Company : BaseEntity
    {
       
        public string IdentificationCode { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Company() { }

        public Company(Company company, List<Product> products)
        {
            IdentificationCode = company.IdentificationCode;
            Name = company.Name;
            Products = products;
        }


    }
}
