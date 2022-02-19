using Domain.Attributes;
using Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    [Auditable]
    public class User
    {
        #region Properties

        public long UserId { get; set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string EmailActiveCode { get; private set; }

        public bool IsEmailActive { get; private set; }

        public string Mobile { get; private set; }

        public string MobileActiveCode { get; private set; }

        public bool IsMobileActive { get; private set; }

        public string Password { get; private set; }

        public string Avatar { get;private set; }


        #endregion

        #region Relations

        public ICollection<RolesUser> RolesUsers { get; set; }

        #endregion

        #region Methods

        public User (string firstName,string lastName, string mobile,string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;
            Password = password;
       
        }

        public void Edit(string firstName, string lastName, string email, string mobile, string avatar,bool isEmailActive,bool isMobileActive)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Mobile = mobile;
            Avatar = avatar;
            IsEmailActive = isEmailActive;
            IsMobileActive = isMobileActive;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
       
        #endregion
    }
}
