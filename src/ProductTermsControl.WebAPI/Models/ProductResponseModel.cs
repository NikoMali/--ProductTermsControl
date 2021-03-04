using ProductTermsControl.Domain.Entities;

namespace ProductTermsControl.WebAPI.Models
{
  public class ProductResponseModel
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
    }
}