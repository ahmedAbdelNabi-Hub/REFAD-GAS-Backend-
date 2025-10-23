using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class FuelRequestConfiguration : IEntityTypeConfiguration<FuelRequest>
    {
        public void Configure(EntityTypeBuilder<FuelRequest> builder)
        {
            builder.ToTable("fuel_requests");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.CompanyId).HasColumnName("company_id").IsRequired();
            builder.Property(f => f.CarId).HasColumnName("car_id").IsRequired();
            builder.Property(f => f.Qty).HasColumnName("qty").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(f => f.Amount).HasColumnName("amount").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(f => f.Station).HasColumnName("station").HasMaxLength(255).IsRequired();
            builder.Property(f => f.RequestDateTime).HasColumnName("request_datetime").HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Property(f => f.Status).HasColumnName("status").HasMaxLength(50).HasDefaultValue("pending");
            builder.Property(f => f.StationId).HasColumnName("station_id");
            builder.Property(f => f.PumpImageBefore).HasColumnName("pump_image_before");
            builder.Property(f => f.PumpImageAfter).HasColumnName("pump_image_after");

        }
    }
}
