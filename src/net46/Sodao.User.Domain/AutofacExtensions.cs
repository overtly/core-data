using Autofac;
using Sodao.User.Domain.Contracts;
using Sodao.User.Domain.Repositories;

namespace Sodao.User.Domain
{
    public static class AutofacExtensions
    {
        public static void AddDomainDI(this ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}
