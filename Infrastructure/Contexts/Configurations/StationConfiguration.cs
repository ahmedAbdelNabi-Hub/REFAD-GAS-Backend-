using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class StationConfiguration : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.ToTable("stations");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.StationId)
                   .HasColumnName("station_id")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.VendorId)
                   .HasColumnName("vendor_id")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                  .HasColumnName("created_at")
                  .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(c => c.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(s => s.LocationLat).HasColumnName("location_lat").IsRequired();
            builder.Property(s => s.LocationLng).HasColumnName("location_lng").IsRequired();
            builder.Property(s => s.StationNameEn).HasColumnName("station_namEn");
            builder.Property(s => s.StationNameAr).HasColumnName("station_nameAr");
            builder.HasOne(s => s.Vendor)
           .WithMany(v => v.Stations)
           .HasForeignKey(s => s.VendorId)
           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
