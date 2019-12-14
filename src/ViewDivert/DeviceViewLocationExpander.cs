using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Myvas.AspNetCore.ViewDivert
{
	public class DeviceViewLocationExpander : IViewLocationExpander
	{
		private readonly IDeviceResolver _deviceResolver;
		private readonly ViewDivertOptions _options;

		/// <summary>
		/// Instantiates a new <see cref="DefaultTagHelperActivator" /> instance.
		/// </summary>
		/// <param name="optionsAccessor">The <see cref="ViewDivertOptions" />.</param>
		/// <param name="deviceResolver">The device resolver.</param>
		public DeviceViewLocationExpander(IOptions<ViewDivertOptions> optionsAccessor, IDeviceResolver deviceResolver)
		{
			_deviceResolver = deviceResolver ?? throw new ArgumentNullException(nameof(deviceResolver));
			_options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
			if (_options.Indicator == null)
			{
				_options.Indicator = ViewDivertDefaults.Indicator;
			}

		}

		/// <inheritdoc />
		public void PopulateValues(ViewLocationExpanderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			// viewdivert -> deviceCode
			context.Values[_options.Indicator] =
				_deviceResolver.Resolve(context.ActionContext.HttpContext);
		}

		/// <inheritdoc />
		public virtual IEnumerable<string> ExpandViewLocations(
			ViewLocationExpanderContext context,
			IEnumerable<string> viewLocations)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (viewLocations == null)
			{
				throw new ArgumentNullException(nameof(viewLocations));
			}

			if (ContainsViewDivertAttribute(context))
			{
				if (context.Values.TryGetValue(_options.Indicator, out string value))
				{
					if (!string.IsNullOrEmpty(value))
					{
						return TryInsertLocations(viewLocations, value);
					}
				}
			}

			return viewLocations;
		}

		private bool ContainsViewDivertAttribute(ViewLocationExpanderContext context)
		{
			var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
			if (controllerActionDescriptor != null)
			{
				//Controller
				var targetAttribute = controllerActionDescriptor.ControllerTypeInfo?.GetCustomAttribute<ViewDivertAttribute>(inherit: true);
				if (targetAttribute != null)
				{
					return true;
				}

				//Controller/Action
				targetAttribute = controllerActionDescriptor.MethodInfo?.GetCustomAttribute<ViewDivertAttribute>(inherit: true);
				if (targetAttribute != null)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 选择视图文件
		/// </summary>
		/// <param name="viewLocations"></param>
		/// <param name="code">View区别码</param>
		/// <returns></returns>
		private IEnumerable<string> TryInsertLocations(IEnumerable<string> viewLocations, string code)
		{
			foreach (var location in viewLocations)
			{
				if (_options.Format == ViewDivertLocationExpanderFormat.SubFolder)
				{
					var newLocation = location.Replace("{0}", code + "/{0}");
					yield return newLocation;
				}
				else
				{
					var newLocation = location.Replace("{0}", "{0}." + code);
					yield return newLocation;
				}

				yield return location;
			}
		}
	}
}