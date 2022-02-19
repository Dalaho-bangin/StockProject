using Domain.ProductCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.EntityConfigurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => x.ProductCategoryId);
           
            builder.Property(x => x.ProductCategoryName).IsRequired().HasMaxLength(400);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(400);
           
        }
    }
}
