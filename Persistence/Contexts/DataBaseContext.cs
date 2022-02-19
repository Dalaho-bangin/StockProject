using Application.Interfaces.Context;
using Domain.Attributes;
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
using Persistence.EntityConfigurations;
using Persistence.Seeds;
using System;
using System.Linq;

namespace Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }


        #region Entities

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolesUser> RolesUsers { get; set; }
        public DbSet<PermissionsRole> PermissionsRoles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        #region Products

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }
        public DbSet<ProductSize> productSizes { get; set; }
        public DbSet<ProductPicture> productPictures { get; set; }
        public DbSet<ProductSelectedProductCategory> productSelectedProductCategories { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemoved").HasDefaultValue(false);
                }
            }

            #region Query Filter

            builder.Entity<User>()
               .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<Role>()
                 .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<ProductCategory>()
                  .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<Product>()
                 .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<ProductColor>()
                 .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<ProductSize>()
                 .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<ProductFeature>()
                 .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<ProductPicture>()
                 .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            #endregion

            #region Seed Data

            DataBaseContextSeed.PermissionSeed(builder);

            #endregion

            #region Apply Configuration

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new ProductCategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductSelectedProductCategoryConfiguration());

            #endregion


        }


        #region Save Changes

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted
                );
            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var updated = entityType.FindProperty("UpdateTime");
                    var RemoveTime = entityType.FindProperty("RemoveTime");
                    var IsRemoved = entityType.FindProperty("IsRemoved");
                    if (item.State == EntityState.Added && inserted != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                    }
                    if (item.State == EntityState.Modified && updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && RemoveTime != null && IsRemoved != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemoved").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }

            }
            return base.SaveChanges();
        }

        #endregion
    }
}
