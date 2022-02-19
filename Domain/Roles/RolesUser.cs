using Domain.Users;

namespace Domain.Roles
{
    public class RolesUser
    {
        public long RolesUserId { get; set; }

        public long RoleId { get;  set; }

        public long UserId { get;  set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}