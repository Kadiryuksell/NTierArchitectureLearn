using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NTierArchitecture.Business.Behaviors;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddMediatR(cfr =>
            {
                cfr.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly,typeof(AppUser).Assembly);
                cfr.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            
            return services;
        }
    }
}
