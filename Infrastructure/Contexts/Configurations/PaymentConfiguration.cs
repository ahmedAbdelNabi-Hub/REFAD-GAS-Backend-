using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CompanyId).HasColumnName("company_id").IsRequired();
            builder.Property(p => p.Amount).HasColumnName("amount").HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.PaymentType).HasColumnName("payment_type").HasMaxLength(50).IsRequired();
            builder.Property(p => p.TransactionDate).HasColumnName("transaction_date").HasDefaultValueSql("SYSDATETIMEOFFSET()");
            builder.Property(p => p.Status).HasColumnName("status").HasMaxLength(50).HasDefaultValue("Pending");
            builder.Property(p => p.ServiceType).HasColumnName("service_type").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasColumnName("description");
            builder.Property(p => p.ReferenceNumber).HasColumnName("reference_number");
        }
    }
}
