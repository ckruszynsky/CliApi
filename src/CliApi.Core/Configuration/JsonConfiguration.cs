using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CliApi.Core.Configuration
{
    public static class ControllerConfiguration
    {
        public static void AddControllersWithJsonContracts(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }
    }
}
