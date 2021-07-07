using Autofac;
using Overt.User.Application.Constracts;
using Overt.User.Application.Services;
using Overt.User.Domain;

namespace Overt.User.Application
{
    public static class AutofacExtensions
    {
        public static void AddApplicationDI(this ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<SubUserService>().As<ISubUserService>();
            builder.RegisterType<SubDbUserService>().As<ISubDbUserService>();
            builder.RegisterType<SubDbUser2Service>().As<ISubDbUser2Service>();

            builder.AddDomainDI();
        }
    }
}