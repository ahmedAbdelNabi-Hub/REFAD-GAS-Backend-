using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("cars");

            builder.HasKey(c => c.Id)
                   .HasName("PK_cars");

            builder.Property(c => c.Id)
                   .HasColumnName("id")
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            builder.Property(c => c.CompanyId)
                   .HasColumnName("company_id")
                   .IsRequired();

            builder.Property(c => c.PlateNumber)
                   .HasColumnName("plate_number")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(c => c.PlateNumber)
                   .IsUnique();

            builder.Property(c => c.CarType)
                   .HasColumnName("car_type")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.StartDay)
                   .HasColumnName("start_day") 
                   .IsRequired();

            builder.Property(c => c.FuelType)
                   .HasColumnName("fuel_type")
                   .HasMaxLength(20)
                   .HasDefaultValue("91");

            builder.Property(c => c.Status)
                   .HasColumnName("status")
                   .HasMaxLength(20)
                   .HasDefaultValue("active");

            builder.Property(c => c.ControlType)
                   .HasColumnName("control_type")
                   .HasMaxLength(20)
                   .HasDefaultValue("Monthly");

            builder.Property(c => c.LimitQty)
                   .HasColumnName("limit_qty")
                   .HasDefaultValue(0);

            builder.Property(c => c.DriverName)
                   .HasColumnName("driver_name")
                   .HasMaxLength(255);

            builder.Property(c => c.DriverMobile)
                   .HasColumnName("driver_mobile")
                   .HasMaxLength(50);

            builder.Property(c => c.DriverPassword)
                   .HasColumnName("driver_password")
                   .HasMaxLength(255);

            builder.Property(c => c.DriverImageUrl)
                   .HasColumnName("driver_image_url")
                   .HasMaxLength(500);

            builder.Property(c => c.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(c => c.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");


        builder.HasOne(c => c.Company)
                    .WithMany(cmp => cmp.Cars)
                    .HasForeignKey(c => c.CompanyId)
                    .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.FuelRequests)
                   .WithOne()
                   .HasForeignKey("CarId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
