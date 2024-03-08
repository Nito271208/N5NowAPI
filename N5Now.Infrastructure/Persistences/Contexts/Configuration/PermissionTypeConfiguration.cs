using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5Now.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Now.Infrastructure.Persistences.Contexts.Configuration
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07198F3F5C");

            builder.HasIndex(e => e.Id, "IX_PermissionTypes_Id");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.PermissionDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
