using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AspNetCore.ViewDivertMiddleware
{
    public static class ViewDivertMiddlewareExtensions
    {
        public static IServiceCollection AddViewDivert(this IServiceCollection services)
            => services.AddViewDivert(options=> { });

        public static IServiceCollection AddViewDivert(this IServiceCollection services,
            Action<ViewDivertOptions> optionsAction)
        {
            services.Configure(optionsAction);
            
            services.TryAddTransient<IDeviceResolver, AgentResolver>();
            services.TryAddTransient<DeviceViewLocationExpander>();
            
            services.Configure<RazorViewEngineOptions>(
                options =>
                {
                    options.ViewLocationExpanders.Add(
                        services.BuildServiceProvider().GetService<DeviceViewLocationExpander>());
                });

            return services;
        }
    }

}
