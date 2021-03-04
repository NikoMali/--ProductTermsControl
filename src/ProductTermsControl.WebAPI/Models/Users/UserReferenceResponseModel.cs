using ProductTermsControl.Domain.Entities;

namespace ProductTermsControl.WebAPI.Models.Users
{
  public class UserReferenceResponseModel
    {
       
        public int UserId { get; set; }
       
        public UserModel User { get; set; }

        
        public int MagazineBranchId { get; set; }
        public MagazineBranchModel MagazineBranchs { get; set; }

        public int PositionId { get; set; }
        public PositionModel Positions { get; set; }

    }
}