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

            services.AddDomainDI();
        }
    }
}
