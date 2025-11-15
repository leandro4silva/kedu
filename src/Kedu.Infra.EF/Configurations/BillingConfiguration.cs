using System.Diagnostics.CodeAnalysis;
using Kedu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kedu.Infra.Data.EF.Configurations
{
    [ExcludeFromCodeCoverage]
    public class BillingConfiguration : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Value)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(b => b.DueDate)
                .IsRequired();

            builder.Property(b => b.PaymentMethod)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(b => b.StatusBilling)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(b => b.PaymentCode)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasOne(b => b.PaymentPlan)
                .WithMany(p => p.Billings)
                .HasForeignKey(b => b.PaymentPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(b => b.PaymentCode);
        }
    }
}