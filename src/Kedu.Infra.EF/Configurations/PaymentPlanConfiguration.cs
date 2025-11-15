using System.Diagnostics.CodeAnalysis;
using Kedu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kedu.Infra.Data.EF.Configurations
{
    [ExcludeFromCodeCoverage]
    public class PaymentPlanConfiguration : IEntityTypeConfiguration<PaymentPlan>
    {
        public void Configure(EntityTypeBuilder<PaymentPlan> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.TotalValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.HasOne(p => p.FinancialManager)
                .WithMany()
                .HasForeignKey(p => p.FinancialManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CostCenter)
                .WithMany()
                .HasForeignKey(p => p.CostCenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Billings)
                .WithOne(b => b.PaymentPlan)
                .HasForeignKey(b => b.PaymentPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}