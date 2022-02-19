using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(400);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(400);
            builder.Property(u => u.Mobile).IsRequired().HasMaxLength(15);
            builder.Property(u => u.Password).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(400);


        }
    }
}
