using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration;
using CliApi.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CliApi.Core.Configuration
{
    public static class EntityFrameworkCoreConfiguration
    {
        public static void AddEntityFrameWorkCore(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration, string connectionStringName)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                ConnectionString = configuration.GetConnectionString(name: connectionStringName),
                Username = configuration["UserID"],
                Password = configuration["Password"]
            };

            services.AddDbContext<CommandContext>
            (
                opt =>
                {
                    opt.UseNpgsql(builder.ConnectionString);
                }
            );
        }
    }
}
