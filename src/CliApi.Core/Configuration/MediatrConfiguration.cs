
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;

namespace CliApi.Core.Configuration
{
    public static class MediatrConfiguration
    {
         public static void AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}