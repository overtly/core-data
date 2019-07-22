using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Sodao.User.Application;
using Sodao.User.Application.Constracts;

namespace Sodao.Core.DataConsole
{
    class Program
    {
        static IServiceProvider provider;
        static Program()
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory)
                   .AddJsonFile("appsettings.json", true);

            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddApplicationDI();

            provider = services.BuildServiceProvider();

        }
        static void Main(string[] args)
        {
            var _userService = provider.GetService<IUserService>();
            _userService.DoSomethingWithTrans();

            _userService.GetList();
            _userService.GetByIds();

            //_userService.DoSomething();
        }
    }
}
