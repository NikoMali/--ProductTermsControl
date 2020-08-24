namespace ProductTermsControl.WebAPI.Models
{
  public class MagazineBranchModel
    {
        public int Id { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int MagazineId { get; set; }
    }
}