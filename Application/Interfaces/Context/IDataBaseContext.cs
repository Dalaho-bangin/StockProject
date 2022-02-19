using Domain.ProductCategories;
using Domain.ProductColors;
using Domain.ProductFeatures;
using Domain.ProductPictures;
using Domain.Products;
using Domain.ProductSelectedProductCategories;
using Domain.ProductSizes;
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

        #region Products

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }
        public DbSet<ProductSize> productSizes { get; set; }
        public DbSet<ProductPicture> productPictures { get; set; }
        public DbSet<ProductSelectedProductCategory> productSelectedProductCategories { get; set; }

        #endregion

    }
}
