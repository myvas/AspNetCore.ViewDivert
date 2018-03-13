using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ViewDivertMiddleware
{
    public interface IDeviceSwitcher
    {
        int Priority { get; }
        IDevice LoadPreference(HttpContext context);
        void StoreDevice(HttpContext context, IDevice device);
        void ResetStore(HttpContext context);
    }
}
