using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Roles
{
    public class Permission
    {
        public long PermissionId { get;  set; }

        public string PermissionTitle { get;  set; }

        public long? ParentPermissionId { get;  set; }

        public ICollection<Permission> Permissions { get; set; }

    }
}
