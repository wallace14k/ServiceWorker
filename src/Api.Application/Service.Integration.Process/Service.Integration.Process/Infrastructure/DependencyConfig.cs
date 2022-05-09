using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Integration.Process.Infrastructure.Repository;
using Service.Integration.Process.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Service.Integration.Process.Infrastructure
{
    public static class DependencyConfig
    {
        public static IServiceCollection Dependency (IServiceCollection services)
        {
            services.AddScoped<IPontWorkRepository, PontWorkRepository>();
            services.AddScoped<IPointWorkService, PointWorkService>();
            return services;
        }    
    }
}
