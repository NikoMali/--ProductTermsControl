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
        public int MagazineBranchId { get; set; }
        public int PositionId { get; set; }
    }
}