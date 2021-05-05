using System.Collections.Generic;

namespace ProductTermsControl.WebAPI.Models
{
  public class CompanyModel
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}