namespace Domain.Roles
{
    public class PermissionsRole
    {
        public long PermissionsRoleId { get; set; }

        public long RoleId { get;  set; }

        public long PermissionId { get;  set; }

        public Role Role { get; set; }

        public Permission Permission { get; set; }
    }
}