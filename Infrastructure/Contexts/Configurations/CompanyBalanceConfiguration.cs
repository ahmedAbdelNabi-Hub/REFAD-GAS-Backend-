using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class CompanyBalanceConfiguration : IEntityTypeConfiguration<CompanyBalance>
    {
        public void Configure(EntityTypeBuilder<CompanyBalance> builder)
        {
            builder.ToTable("company_balances");

            builder.HasKey(cb => cb.Id)
                   .HasName("PK_company_balances");

            builder.Property(cb => cb.Id)
                   .HasColumnName("id")
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            builder.Property(cb => cb.CompanyId)
                   .HasColumnName("company_id")
                   .IsRequired();

        

            builder.HasIndex(cb => cb.CompanyId)
                   .IsUnique(); // one-to-one

            builder.Property(cb => cb.CurrentBalance)
                   .HasColumnName("current_balance")
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(cb => cb.FuelBalance)
                   .HasColumnName("fuel_balance")
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(cb => cb.ServicesBalance)
                   .HasColumnName("services_balance")
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0.00m);

            builder.Property(cb => cb.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property(cb => cb.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.HasOne(cb => cb.Company)
                   .WithOne(c => c.Balance)
                   .HasForeignKey<CompanyBalance>(cb => cb.CompanyId)
                   .HasConstraintName("FK_company_balances_company")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
