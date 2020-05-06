using Autofac;
using AutoMapper;
using Overt.User.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overt.Core.DataConsole
{
    public class AutofacContainer
    {
        public static IContainer Container { get; private set; }

        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            builder.AddApplicationDI();


            builder.RegisterType<AutoMapperProfiles>().As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

            Container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
            return Container;
        }
    }
}
