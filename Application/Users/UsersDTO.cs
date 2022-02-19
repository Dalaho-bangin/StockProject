using Common.Paging;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UsersDTO
    {
        public long UserId { get; set; }

        public string FirstName { get;  set; }

        public string LastName { get;  set; }

        public string Email { get;  set; }

        public bool IsEmailActive { get;  set; }

        public string Mobile { get;  set; }

        public bool IsMobileActive { get;  set; }

        public string Avatar { get;  set; }

    }
}
