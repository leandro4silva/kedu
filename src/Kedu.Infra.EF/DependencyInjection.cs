using System.Diagnostics.CodeAnalysis;
using Kedu.Domain.Repositories;
using Kedu.Domain.SeedWork;
using Kedu.Infra.Configurations;
using Kedu.Infra.Context;
using Kedu.Infra.Data.EF.Repositories.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uof = Kedu.Infra.Data.EF.UnitOfWork;

namespace Kedu.Infra.Data.EF
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(
        this IServiceCollection services, AppConfiguration appConfiguration)
        {
            services.AddScoped<IUnitOfWork, Uof.UnitOfWork>();

            services.AddScoped<IFinancialManagerRepository, FinancialManagerRepository>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<ICostCenterRepository, CostCenterRepository>();
            services.AddScoped<IPaymentPlanRepository, PaymentPlanRepository>();

            return services;
        }

        public static IServiceCollection AddAppConnections(
            this IServiceCollection services,
            AppConfiguration appConfiguration
        )
        {
            services.AddDbConnection(appConfiguration);
            return services;
        }

        private static IServiceCollection AddDbConnection(this IServiceCollection services, AppConfiguration appConfiguration)
        {
            if (appConfiguration.PostgreSql == null)
            {
                throw new ArgumentNullException(nameof(appConfiguration.PostgreSql));
            }

            if (string.IsNullOrEmpty(appConfiguration.PostgreSql.ConnectionString))
            {
                throw new ArgumentNullException(nameof(appConfiguration.PostgreSql.ConnectionString));
            }

            services.AddDbContext<KeduDbContext>(options =>
                options.UseNpgsql(
                    appConfiguration.PostgreSql.ConnectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.CommandTimeout(30); 
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorCodesToAdd: null
                        );
                    }
                ));

            return services;
        }
    }
}
