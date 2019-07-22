using Autofac;
using Sodao.User.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodao.Core.DataConsole
{
    public class AutofacContainer
    {
        public static IContainer Container { get; private set; }

        public static IContainer Register()
        {
            var builder = new ContainerBuilder();
            builder.AddApplicationDI();
            Container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
            return Container;
        }
    }
}
