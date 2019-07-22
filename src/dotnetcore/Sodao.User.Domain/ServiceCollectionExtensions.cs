using Microsoft.Extensions.DependencyInjection;
using Sodao.User.Domain.Contracts;
using Sodao.User.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sodao.User.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainDI(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
