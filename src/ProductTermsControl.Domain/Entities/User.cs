using System;
using System.Collections.Generic;

namespace ProductTermsControl.Domain.Entities
{
    public class User : BaseEntity
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string MobileNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        




        //referencee

        public List<UserReference> UserReferences { get; set; }
        public List<ResponsiblePersonsForProduct> ResponsiblePersonsByProducts { get; set; }
        public List<UserActivity> UserActivities { get; set; }


        public User() { }

        public User(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Avatar = user.Avatar;
            MobileNumber = user.MobileNumber;
        }

        public void UpdateOtherProperies(User user)
        {
            Email = user.Email;
            Avatar = user.Avatar;
            MobileNumber = user.MobileNumber;
        }
    }
}