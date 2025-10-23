using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder.HasKey(c => c.Id)
                   .HasName("PK_companies");

            builder.Property(c => c.Id)
                   .HasColumnName("id")
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            builder.Property(c => c.CompanyNameArabic)
                   .HasColumnName("company_name_arabic")
                   .HasMaxLength(255)
                   .IsRequired();


            builder.Property(c => c.LogoPath)
                   .HasColumnName("logo_path")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(c => c.DocumentsPaths)
                .HasColumnName("documents_paths")
                .HasMaxLength(255)
                .IsRequired();


            builder.Property(c => c.CompanyNameEnglish)
                   .HasColumnName("company_name_english")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(c => c.ResponsiblePerson)
                   .HasColumnName("responsible_person")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(c => c.IdentityId)
                   .HasColumnName("identity_id")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.Mobile)
                   .HasColumnName("mobile")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(c => c.Email)
                   .HasColumnName("email")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.HasIndex(c => c.Email)
                   .IsUnique();

            builder.Property(c => c.PasswordHash)
                   .HasColumnName("password_hash")
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(c => c.Address)
                   .HasColumnName("address")
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(c => c.PumpImageRequired)
                   .HasColumnName("pump_image_required")
                   .HasDefaultValue(false);

            builder.Property(c => c.CarImageRequired)
                   .HasColumnName("car_image_required")
                   .HasDefaultValue(false);

            builder.Property(c => c.CarLimitType)
                   .HasColumnName("car_limit_type")
                   .HasMaxLength(50)
                   .HasDefaultValue("monthly");

            builder.Property(c => c.CarLimitCount)
                   .HasColumnName("car_limit_count")
                   .HasDefaultValue(0);

            builder.Property(c => c.MonthlyCostPerCar)
                   .HasColumnName("monthly_cost_per_car")
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(c => c.Status)
                   .HasColumnName("status")
                   .HasMaxLength(50)
                   .HasDefaultValue("pending");

            builder.Property(c => c.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(c => c.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            // Relationships (optional, depending on your models)
       
            builder.HasMany(c => c.Cars)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyId)
                   .HasPrincipalKey(c => c.Id)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_cars_company");

            builder.HasOne(c => c.Balance)
                   .WithOne()
                   .HasForeignKey("CompanyBalance", "CompanyId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.FuelRequests)
                   .WithOne()
                   .HasForeignKey("CompanyId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Payments)
                   .WithOne()
                   .HasForeignKey("CompanyId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
