using ProductTermsControl.Application.Services;
using ProductTermsControl.Domain.Entities;

namespace ProductTermsControl.WebAPI.Models
{
  public class BranchUserModel
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string MobileNumber { get; set; }
        
        public int PositionId { get; set; }
        public string Position { get; set; }


       public BranchUserModel(User user, IUserService _userService)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Avatar = user.Avatar;
            MobileNumber = user.MobileNumber;
            PositionId = _userService.PositionGetById(_userService.UserReferenceGetById(user.Id).GetAwaiter().GetResult().PositionId).GetAwaiter().GetResult().Id;
            Position = _userService.PositionGetById(_userService.UserReferenceGetById(user.Id).GetAwaiter().GetResult().PositionId).GetAwaiter().GetResult().Name;
        }

       
    }
}