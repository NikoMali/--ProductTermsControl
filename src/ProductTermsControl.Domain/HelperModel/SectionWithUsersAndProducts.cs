using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Domain.HelperModel
{
    public class SectionWithUsersAndProducts
    {
        public ResponsiblePersonsGroup Section { get; set; }
        public List<User> users { get; set; }
        public List<Product> products { get; set; }


        public SectionWithUsersAndProducts()
        {

        }
        public SectionWithUsersAndProducts(ResponsiblePersonsGroup responsiblePersons, List<User> users, List<Product> products)
        {
            Section = responsiblePersons;
            this.users = users;
            this.products = products;
        }
    }
}
