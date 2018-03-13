using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;

namespace AspNetCore.ViewDivertMiddleware
{
    public class UrlSwitcher : IDeviceSwitcher
    {
        private readonly IDeviceFactory _deviceFactory;
        private readonly IDeviceRedirector _deviceRedirector;
        private readonly IOptions<ViewDivertOptions> _options;

        public UrlSwitcher(IOptions<ViewDivertOptions> options, IDeviceFactory deviceFactory, IDeviceRedirector deviceRedirector)
        {
            _options = options;
            _deviceFactory = deviceFactory;
            _deviceRedirector = deviceRedirector;
        }

        public int Priority => 2;

        public IDevice LoadPreference(HttpContext context)
        {
            var url = context.Request.GetDisplayUrl();

            if (url.Contains($"//{_options.Value.MobileCode}."))
            {
                return _deviceFactory.Mobile();
            }

            if (url.Contains($"//{_options.Value.TabletCode}."))
            {
                return _deviceFactory.Tablet();
            }

            return null;
        }

        public void StoreDevice(HttpContext context, IDevice device)
            => _deviceRedirector.RedirectToDevice(context, device.DeviceCode);

        public void ResetStore(HttpContext context)
            => _deviceRedirector.RedirectToDevice(context);
    }
}