using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;

namespace AspNetCore.ViewDivertMiddleware
{
    public class DeviceViewLocationExpander : IViewLocationExpander
    {
        private readonly IDeviceResolver _deviceResolver;
        private readonly ViewDivertOptions _options;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Instantiates a new <see cref="DefaultTagHelperActivator" /> instance.
        /// </summary>
        /// <param name="optionsAccessor">The <see cref="ViewDivertOptions" />.</param>
        /// <param name="deviceResolver">The device resolver.</param>
        public DeviceViewLocationExpander(IOptions<ViewDivertOptions> optionsAccessor, IDeviceResolver deviceResolver,IHostingEnvironment env)
        {
            _deviceResolver = deviceResolver ?? throw new ArgumentNullException(nameof(deviceResolver));
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
            _env = env ?? throw new ArgumentNullException(nameof(env));
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
                string value;
                if (context.Values.TryGetValue(_options.Indicator, out value))
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
                var actionAttributes = controllerActionDescriptor.MethodInfo?.GetCustomAttributes(inherit: true);
                foreach (var attribute in actionAttributes)
                {
                    if (attribute.GetType() == typeof(ViewDivertAttribute))
                    {
                        return true;
                    }
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