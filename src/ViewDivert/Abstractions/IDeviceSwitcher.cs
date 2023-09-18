using Microsoft.AspNetCore.Http;

namespace Myvas.AspNetCore.ViewDivert
{
	public interface IDeviceSwitcher
    {
        int Priority { get; }
        IDevice LoadPreference(HttpContext context);
        void StoreDevice(HttpContext context, IDevice device);
        void ResetStore(HttpContext context);
    }
}
