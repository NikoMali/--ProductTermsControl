using ProductTermsControl.Domain.Entities;
using ProductTermsControl.WebAPI.Models.Magaziness;

namespace ProductTermsControl.WebAPI.Models
{
  public class MagazineBranchResponseModel
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MagazineId { get; set; }
        public MagazineModel Magazine { get; set; }
    }
}