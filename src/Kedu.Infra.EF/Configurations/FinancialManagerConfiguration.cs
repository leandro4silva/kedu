using System.Diagnostics.CodeAnalysis;
using Kedu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kedu.Infra.Data.EF.Configurations
{
    [ExcludeFromCodeCoverage]
    public class FinancialManagerConfiguration : IEntityTypeConfiguration<FinancialManager>
    {
        public void Configure(EntityTypeBuilder<FinancialManager> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}