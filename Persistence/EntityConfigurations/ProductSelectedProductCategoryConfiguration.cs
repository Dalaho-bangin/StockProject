using Domain.ProductSelectedProductCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ProductSelectedProductCategoryConfiguration : IEntityTypeConfiguration<ProductSelectedProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductSelectedProductCategory> builder)
        {
            builder.HasKey(x => x.ProductSelectedProductCategoryId);

            builder
            .HasOne(p => p.Product)
            .WithMany(t => t.ProductSelectedProductCategories)
            .HasForeignKey(f => f.ProductId);

            builder
               .HasOne(p => p.ProductCategory)
               .WithMany(t => t.ProductSelectedProductCategories)
               .HasForeignKey(f => f.ProductCategoryId);

        }
    }
}
