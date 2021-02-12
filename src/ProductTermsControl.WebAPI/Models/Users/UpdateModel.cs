using ProductTermsControl.Domain.Entities;

namespace ProductTermsControl.WebAPI.Models.Users
{
  public class UpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string MobileNumber { get; set; }
        public int? MagazineBranchId { get; set; }
        public int? PositionId { get; set; }


        public UpdateModel()
        {

        }

        public UpdateModel(User user, UserReference userReference)
        {
            
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Avatar = user.Avatar;
            MobileNumber = user.MobileNumber;
            MagazineBranchId = userReference?.MagazineBranchId;
            PositionId = userReference?.PositionId;
        }
    }
}