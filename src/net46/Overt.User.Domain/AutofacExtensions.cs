using Autofac;
using Overt.User.Domain.Contracts;
using Overt.User.Domain.Repositories;

namespace Overt.User.Domain
{
    public static class AutofacExtensions
    {
        public static void AddDomainDI(this ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<SubUserRepository>().As<ISubUserRepository>();
            builder.RegisterType<SubDbUserRepository>().As<ISubDbUserRepository>();
        }
    }
}
