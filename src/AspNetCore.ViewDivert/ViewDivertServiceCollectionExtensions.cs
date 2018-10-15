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
using Myvas.AspNetCore.ViewDivert;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ViewDivertServiceCollectionExtensions
    {
        public static IServiceCollection AddViewDivert(this IServiceCollection services,
            Action<ViewDivertOptions> optionsAction = null)
        {
            if (optionsAction != null)
            {
                services.Configure(optionsAction); //IOptions<ViewDivertOptions>
            }
            
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
