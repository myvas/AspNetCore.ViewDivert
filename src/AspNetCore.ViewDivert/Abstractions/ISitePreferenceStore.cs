using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
