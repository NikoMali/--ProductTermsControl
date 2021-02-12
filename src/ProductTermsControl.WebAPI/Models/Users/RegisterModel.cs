using ProductTermsControl.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductTermsControl.WebAPI.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string MobileNumber { get; set; }
        [Required]
        public int MagazineBranchId { get; set; }
        [Required]
        public int PositionId { get; set; }


        public RegisterModel()
        {

        }



        public RegisterModel(User user, UserReference userReference)
        {

            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Avatar = user.Avatar;
            MobileNumber = user.MobileNumber;
            MagazineBranchId = userReference.MagazineBranchId;
            PositionId = userReference.PositionId;
        }
    }
}