using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliApi.Core.Data;
using CliApi.Core.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace CliApi.Core.Configuration
{
    public static class CoreConfiguration
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddScoped<IDbContextResolver, DbContextResolver<CommandContext>>();
        }
    }
}
