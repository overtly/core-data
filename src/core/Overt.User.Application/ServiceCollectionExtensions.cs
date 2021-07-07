using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Overt.User.Application.Constracts;
using Overt.User.Application.Services;
using Overt.User.Domain;

namespace Overt.User.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationDI(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISubUserService, SubUserService>();
            services.AddTransient<ISubDbUserService, SubDbUserService>();
            services.AddTransient<ISubDbUser2Service, SubDbUser2Service>();

            services.AddDomainDI();
            services.AddAutoMapper();
        }
    }
}
