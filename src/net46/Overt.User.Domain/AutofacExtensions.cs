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
        }
    }
}
