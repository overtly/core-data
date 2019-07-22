using Microsoft.Extensions.DependencyInjection;
using Sodao.User.Application.Constracts;
using Sodao.User.Application.Services;
using Sodao.User.Domain;

namespace Sodao.User.Application
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
