using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class AddUserRolesDTo
    {
        public long UserId { get; set; }

        public IEnumerable<long> RolesId { get; set; }
    }
}
