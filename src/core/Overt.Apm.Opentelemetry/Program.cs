using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Instrumentation.SqlClient;
using Overt.Apm.Opentelemetry.Instrumention.Implementation;
using Overt.Apm.Opentelemetry.Repository;
using Overt.Core.Data.Diagnostics;
using System;
using System.Diagnostics;

namespace Overt.Apm.Opentelemetry
{
    class Program
    {
        static ActivitySource source = new ActivitySource("Demo");
        static void Main(string[] args)
        {


            var subcriber = new CoreDataSubscriber(listener => listener.Name == DiagnosticListenerNames.DiagnosticSourceName);
            subcriber.Subscribe();

            using var tracerProvider = Sdk.CreateTracerProviderBuilder()
          //.AddSqlClientInstrumentation(options =>
          //{
          //    options.Enrich = (activity, str, obj) =>
          //    {
          //        activity.SetTag(str, obj);
          //    };
          //})
          .AddSource(CoreDataActivitySourceHelper.ActivitySourceName)
          .AddSource("Demo")
          .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Demo"))
          .AddConsoleExporter()
          .Build();

            var builder = new ConfigurationBuilder()
                   .SetBasePath(AppContext.BaseDirectory)
                   .AddJsonFile("appsettings.json", true);

            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<IUserRepository, UserRepository>();

            var provider = services.BuildServiceProvider();

            var activity = source.StartActivity("DemoAct", ActivityKind.Server);
            var repository = provider.GetService<IUserRepository>();
            //repository.Add(new Domain.User() { Id = 12, Name = "test2" });
            repository.CountAsync(s => s.Id == 1).GetAwaiter().GetResult();
            activity.Stop();

            //var builder=Sdk.CreateTracerProviderBuilder().ad

            var host = new HostBuilder()
                .Build();
            host.Run();


        }
    }
}
