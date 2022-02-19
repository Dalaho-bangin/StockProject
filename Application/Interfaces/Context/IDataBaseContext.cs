using Domain.ProductCategories;
using Domain.Roles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;


namespace Application.Interfaces.Context
{
    public interface IDataBaseContext
    {
        int SaveChanges();
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolesUser> RolesUsers { get; set; }
        DbSet<PermissionsRole> PermissionsRoles { get; set; }

        #region Product Categories

         DbSet<ProductCategory> ProductCategories { get; set; }

        #endregion

    }
}
