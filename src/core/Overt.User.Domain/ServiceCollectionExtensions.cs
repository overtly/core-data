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
        }
    }
}
