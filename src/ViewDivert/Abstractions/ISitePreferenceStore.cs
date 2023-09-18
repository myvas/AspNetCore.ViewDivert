using Microsoft.AspNetCore.Http;

namespace Myvas.AspNetCore.ViewDivert
{

	public interface ISitePreferenceStore
    {
        string LoadPreference(HttpContext context);
        void SavePreference(HttpContext context, IDevice device);
        void ResetPreference(HttpContext context);
    }
}
