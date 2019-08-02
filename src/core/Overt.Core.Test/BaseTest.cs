using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overt.User.Application;
using System;

namespace Overt.Core.Test
{
    public class BaseTest
    {
        public IServiceProvider provider;
        public BaseTest()
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
    }
}
