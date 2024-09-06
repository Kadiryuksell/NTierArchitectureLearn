using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.DataAccess.Repositories;
using NTierArchitecture.Entities.Models;
using NTierArchitecture.Entities.Repositories;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration) 
        {
            string? connectionString = configuration.GetConnectionString("SqlServer");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("SqlServer is null or empty.");
            }

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            services.AddIdentityCore<AppUser>(cfr =>
            {
                cfr.Password.RequireNonAlphanumeric = false;
            });

            services.AddScoped<IUnitOfWork>(sv => sv.GetRequiredService<ApplicationDbContext>());

            services.Scan(selector => selector
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

            return services;

        }
    }
}
