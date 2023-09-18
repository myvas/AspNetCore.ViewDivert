using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Myvas.AspNetCore.ViewDivert
{
	public class SitePreferenceRepository : ISitePreferenceStore
    {
        private readonly IDeviceResolver _deviceResolver;
        private readonly IOptions<SwitcherOptions> _options;
        private readonly IEnumerable<IDeviceSwitcher> _switchers;

        public SitePreferenceRepository(IEnumerable<IDeviceSwitcher> switchers, IOptions<SwitcherOptions> options,
            IDeviceResolver deviceResolver)
        {
            _switchers = switchers;
            _options = options;
            _deviceResolver = deviceResolver;
        }

        public string LoadPreference(HttpContext context)
            => _switchers
                .OrderByDescending(t => t.Priority)
                .Select(t => t.LoadPreference(context))
                .FirstOrDefault(t => t != null)?.DeviceCode ?? _deviceResolver.Resolve(context);

        public void ResetPreference(HttpContext context) => _options.Value.DefaultSwitcher.ResetStore(context);

        public void SavePreference(HttpContext context, IDevice device)
            => _options.Value.DefaultSwitcher.StoreDevice(context, device);
    }
}