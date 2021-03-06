using ProductTermsControl.Application.Services;
using ProductTermsControl.Domain.Entities;

namespace ProductTermsControl.WebAPI.Models.Users
{
  public class UserModel
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string MobileNumber { get; set; }
        public int? MagazineBranchId { get; set; }
        public string? MagazineBranch { get; set; }
        public int? PositionId { get; set; }
        public string? Position { get; set; }

        


        public UserModel() { }

        public UserModel(User user, UserReference userReference)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Avatar = user.Avatar;
            MobileNumber = user.MobileNumber;
            MagazineBranchId = userReference?.MagazineBranchId;
            if (MagazineBranchId != null && MagazineBranchId != 0)
            {
                MagazineBranch = userReference.MagazineBranchs.Name;
            }
            PositionId = userReference?.PositionId;
            if (PositionId != null && PositionId != 0)
            {
                Position = userReference.Positions.Name;
            }
        }
    }
}