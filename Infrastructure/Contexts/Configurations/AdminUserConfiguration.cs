using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.ToTable("admin_users");

            builder.HasKey(a => a.Id)
                   .HasName("PK_admin_users");

            builder.Property(a => a.Id)
                   .HasColumnName("id")
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            builder.Property(a => a.Email)
                   .HasColumnName("email")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.HasIndex(a => a.Email)
                   .IsUnique();

            builder.Property(a => a.PasswordHash)
                   .HasColumnName("password_hash")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(a => a.FullName)
                   .HasColumnName("full_name")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(a => a.Role)
                   .HasColumnName("role")
                   .HasMaxLength(50)
                   .HasDefaultValue("admin");

            builder.Property(a => a.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(a => a.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");
        }
    }
}
