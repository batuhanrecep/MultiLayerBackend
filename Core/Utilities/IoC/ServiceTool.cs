using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    //Summary: We write this tool to access ServiceProvider with this tool we will control/manage our System services' from central point
    //It will also be our IoC Resolver
    public static class ServiceTool
    {
        //Center of service management object

        public static IServiceProvider ServiceProvider { get; set; } //Central  service management object

        //With this structure, we will access to .Net Core's services via ServiceTool
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
