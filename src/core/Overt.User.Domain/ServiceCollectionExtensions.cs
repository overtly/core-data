using Microsoft.Extensions.DependencyInjection;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Overt.User.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainDI(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISubUserRepository, SubUserRepository>();
            services.AddTransient<ISubDbUserRepository, SubDbUserRepository>();
            services.AddTransient<ISubDbUser2Repository, SubDbUser2Repository>();
        }
    }
}
