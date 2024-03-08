using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5Now.Domain.Entities;

namespace N5Now.Infrastructure.Persistences.Contexts.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasIndex(e => e.PermissionTypeId, "IX_Permissions_PermissionTypeId");

            builder.Property(e => e.EmployeeForename)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.EmployeeSurname)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.PermissionType).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permissio__Permi__5EBF139D");

        }
    }
}
