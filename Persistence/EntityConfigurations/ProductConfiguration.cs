using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(15).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).HasMaxLength(256);
            builder.Property(x => x.PictureTitle).HasMaxLength(500);
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired();

            builder.HasMany(x => x.ProductPictures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ProductColors).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ProductFeatures).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(x => x.ProductSizes).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);


        }
    }
}
