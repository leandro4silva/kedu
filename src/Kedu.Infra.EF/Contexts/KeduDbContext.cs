using System.Diagnostics.CodeAnalysis;
using Kedu.Domain.Entities;
using Kedu.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kedu.Infra.Context
{
    [ExcludeFromCodeCoverage]
    public class KeduDbContext : DbContext
    {
        public DbSet<FinancialManager> FinancialManagers => Set<FinancialManager>();
        public DbSet<Billing> Billings => Set<Billing>();
        public DbSet<CostCenter> CostCenters => Set<CostCenter>();
        public DbSet<PaymentPlan> PaymentPlans => Set<PaymentPlan>();

        public KeduDbContext(DbContextOptions<KeduDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FinancialManagerConfiguration());
            builder.ApplyConfiguration(new BillingConfiguration());
            builder.ApplyConfiguration(new CostCenterConfiguration());
            builder.ApplyConfiguration(new PaymentPlanConfiguration());
        }
    }
}