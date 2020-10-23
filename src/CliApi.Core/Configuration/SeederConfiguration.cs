using System;
using System.Linq;
using CliApi.Core.Data;
using CliApi.Core.Data.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace CliApi.Core.Configuration
{
    public static class SeederConfiguration
    {
        public async  static void ApplyDataSeeds(this IApplicationBuilder app, IWebHostEnvironment environment){
            using (var scope = app.ApplicationServices.CreateScope()){               
               var provider = scope.ServiceProvider.GetService<IDbContextProvider>();

               if(environment.EnvironmentName == "Testing"){
                   await provider.GetContext().Database.EnsureDeletedAsync();
               }            

                 SeedDatabase(provider,environment);
            }
        }

        private static void SeedDatabase(IDbContextProvider provider, IWebHostEnvironment environment)
        {
            var dataSeederInterfaceType = typeof(IDataSeeder);
            var dataSeeders = dataSeederInterfaceType.Assembly
              .ExportedTypes
              .Where(x => dataSeederInterfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(Activator.CreateInstance);

            foreach (IDataSeeder seeder in dataSeeders)
            {
                if (!seeder.ExcludedEnvironments.Contains(environment.EnvironmentName))
                {
                    seeder.Seed(provider);
                }
            }
        }
      
    }
}