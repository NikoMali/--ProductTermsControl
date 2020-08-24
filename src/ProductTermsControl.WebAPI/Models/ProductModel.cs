namespace ProductTermsControl.WebAPI.Models
{
  public class ProductModel
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}