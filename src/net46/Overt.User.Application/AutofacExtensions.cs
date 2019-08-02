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

            builder.AddDomainDI();
        }
    }
}