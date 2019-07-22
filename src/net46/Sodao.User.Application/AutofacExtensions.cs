using Autofac;
using Sodao.User.Application.Constracts;
using Sodao.User.Application.Services;
using Sodao.User.Domain;

namespace Sodao.User.Application
{
    public static class AutofacExtensions
    {
        public static void AddApplicationDI(this ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();

            builder.AddDomainDI();
        }
    }
}