using Domain.Attributes;
using System.Collections.Generic;

namespace Domain.Roles
{
    [Auditable]
    public class Role
    {
        public long RoleId { get;  set; }

        public string RoleTitle { get;  set; }


        public ICollection<RolesUser> RolesUsers { get; set; } 

        public ICollection<PermissionsRole> PermissionsRoles{ get; set; }
    }
}