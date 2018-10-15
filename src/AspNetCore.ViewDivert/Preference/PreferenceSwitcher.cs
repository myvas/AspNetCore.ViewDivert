using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.ViewDivert
{
    public class PreferenceSwitcher
    {
        private readonly IDeviceFactory _deviceFactory;
        private readonly IOptions<SwitcherOptions> _options;
        private readonly ISitePreferenceStore _repository;

        public PreferenceSwitcher(IOptions<SwitcherOptions> options, ISitePreferenceStore repository,
            IDeviceFactory deviceFactory)
        {
            _options = options;
            _repository = repository;
            _deviceFactory = deviceFactory;
        }

        public virtual Task Handle(HttpContext context)
        {
            return Task.Run(() =>
            {
                var device = context.GetRouteValue("device").ToString();
                if (!string.IsNullOrWhiteSpace(device))
                {
                    if (device == _options.Value.MobileKey)
                    {
                        _repository.SavePreference(context, _deviceFactory.Mobile());
                    }
                    else if (device == _options.Value.TabletKey)
                    {
                        _repository.SavePreference(context, _deviceFactory.Tablet());
                    }
                    else if (device == _options.Value.NormalKey)
                    {
                        _repository.SavePreference(context, _deviceFactory.Normal());
                    }
                    else if (device == _options.Value.ResetKey)
                    {
                        _repository.ResetPreference(context);
                    }
                }
            });
        }
    }
}