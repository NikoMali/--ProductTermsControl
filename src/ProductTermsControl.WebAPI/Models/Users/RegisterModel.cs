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
    }
}