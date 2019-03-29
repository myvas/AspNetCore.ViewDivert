using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Myvas.AspNetCore.ViewDivert;
using System;

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

			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.ViewLocationExpanders.Add(
					services.BuildServiceProvider().GetService<DeviceViewLocationExpander>());
			});

			return services;
		}
	}

}
