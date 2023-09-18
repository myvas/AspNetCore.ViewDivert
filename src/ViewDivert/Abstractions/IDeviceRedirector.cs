using Microsoft.AspNetCore.Http;

namespace Myvas.AspNetCore.ViewDivert
{
	public interface IDeviceRedirector
    {
        void RedirectToDevice(HttpContext context, string code = "");
    }
}
