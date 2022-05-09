using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Integration.Process.Infrastructure.Repository;
using Service.Integration.Process.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Service.Integration.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {                   
                    services.AddSingleton<IPointWorkService, PointWorkService>();
                    services.AddSingleton<IPontWorkRepository, PontWorkRepository>();
                    services.AddHostedService<Worker>();

                    var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    services.AddSingleton<IConfiguration>(GetConfiguration(location, "appsettings.json"));
                });

        private static IConfigurationRoot GetConfiguration(string basePath, string file)
        {
            return new ConfigurationBuilder()
             .SetBasePath(basePath)
                 .AddJsonFile(file, optional: true)
                     .Build();
        }
    }
}
