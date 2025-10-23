using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.ToTable("vendors");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.VendorCode)
                   .HasColumnName("vendor_code")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(v => v.VendorNameEn)
                   .HasColumnName("vendor_name")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                  .HasColumnName("created_at")
                  .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(c => c.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(v => v.VendorNameAr).HasColumnName("vendor_name_arabic");
            builder.Property(v => v.ContactEmail).HasColumnName("contact_email");
            builder.Property(v => v.ContactPhone).HasColumnName("contact_phone");
            builder.Property(v => v.HeadquartersAddress).HasColumnName("headquarters_address");
            builder.Property(v => v.LogoUrl).HasColumnName("logo_url");
            builder.Property(v => v.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        }
    }
}
