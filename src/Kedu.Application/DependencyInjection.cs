using FluentValidation;
using Kedu.Application.Common;
using Kedu.Application.Handlers.v1.Handlers;
using Kedu.Application.Handlers.v1.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kedu.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region MediatR

            services.AddMediatR(typeof(CreateFinancialManagerHandler));
            //services.AddAutoMapperProfiles();

            services.AddValidatorsFromAssembly(typeof(CreateFinancialManagerValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            #endregion

            return services;
        }

        //private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(BookProfile));
        //    return services;
        //}
    }
}
